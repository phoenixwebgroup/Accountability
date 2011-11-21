require 'rake'
require 'albacore'

# gem install albacore

class SourceControlRepository

	attr_accessor :path, :isHg, :isGit
	
	def initialize(path)
		@path = path
		setIsHg()
		setIsGit()
	end
	
	def setIsHg
		command = Exec.new()
		command.command = "hg"
		command.parameters = ["status"]
		command.working_directory = @path
		@isHg = command.run_command()
	end
	
	def setIsGit
		command = Exec.new()
		command.command = "git"
		command.parameters = ["status","-s"]
		command.working_directory = @path
		@isGit = command.run_command()
	end
	
	def Clean()
		command = Exec.new()
		command.working_directory = @path
		if @isGit then
			command.command = "git"
			command.parameters = ["clean","-d","-f"]
		elsif @isHg then
			command.command = "hg"
			command.parameters = ["purge","--all"]		
		else 
			ShowUnsupportedRepositoryWarning()
		end
		puts "Cleaning " + @path
		command.run_command "Cleaning lib"
	end

	def PullAndFastForward()
		command = Exec.new()
		if @isHg then
			command.command = "hg"
			command.parameters = ["pull","-u","-R", @path]
		elsif @isGit then
			command.command = "git"
			command.parameters = ["pull","--ff"]
		else
			ShowUnsupportedRepositoryWarning()
		end
		puts "Updating " + command.command +  " repo " + @path
		command.working_directory = @path
		command.run_command "Updating hg Repo"		
	end
	
	def ShowUnsupportedRepositoryWarning
		puts "!!! Unsupported source control repository !!!"
	end
end

class DeployServer
	
	attr_accessor :host, :uploadLocation, :webInstallPath, :serviceInstallPath
	
	def initialize(host, uploadLocation)
		@host = host
		@uploadLocation = uploadLocation
	end

	def rmdir(path)
		puts "Removing remote directory " + path
		sh "psexec.exe \\\\" + @host + " CMD /c RMDIR /s /q \"" + path + "\""
	end
	
	def unzip(fileName, fileUploadLocation, installPath)
		puts "Unzipping " + fileName
		sh "psexec.exe \\\\" + @host + " 7za.exe x -y \"\\\\" + @host + "\\" + fileUploadLocation + "\\" + fileName + "\" -o\"" + installPath + "\""
	end	

	def Copy(file)
		targetDirectory = "\\\\" + File.join(@host, @uploadLocation)
		FileUtils.mkdir_p(targetDirectory) unless File.directory?(targetDirectory)
		puts "Uploading " + file + " to " + targetDirectory
		FileUtils.copy(file, targetDirectory)
	end	

	def DeployWeb(localPath, webZip)
		UploadAndUnzip(localPath, @webInstallPath, webZip)
	end	
	
	def DeployService(localPath, serviceZip, serviceName)
		begin
			UninstallNServiceBusService(serviceName)
		rescue 
		end
		UploadAndUnzip(localPath, @serviceInstallPath, serviceZip)
		InstallNServiceBusService(serviceName)
		serviceManager = ServiceManager.new(serviceName, @host)
		serviceManager.setRestartOnFailures()
		serviceManager.start()
	end
	
	def UninstallNServiceBusService(serviceName)
		puts 'Uninstalling nservice bus service'
		exe = File.join(@serviceInstallPath, 'NServiceBus.Host.exe')
		sh "psexec.exe \\\\" + @host + " -w \"" + @serviceInstallPath + "\" \"" + exe + "\" /uninstall /serviceName:" + serviceName
	end	
	
	def InstallNServiceBusService(serviceName)
		puts 'Installing nservice bus service'
		exe = File.join(@serviceInstallPath, 'NServiceBus.Host.exe')
		sh "psexec.exe \\\\" + @host + " -w \"" + @serviceInstallPath + "\" \"" + exe + "\" /install /serviceName:" + serviceName + " /displayName:" + serviceName
	end
	
	def UploadAndUnzip(localPath, destinationPath, zipFile)
		begin
			rmdir(destinationPath)
		rescue
		end	
		localFile = File.join(localPath, zipFile)
		Copy(localFile)
		unzip(zipFile, @uploadLocation, destinationPath)
	end
end

class PacManager
	
	attr_accessor :pacFolder, :libFolder

	def initialize
		@pacFolder = ENV['PAC']
		@libFolder = "lib"
	end
	
	def copySingleDependency(name, version, file)
		targetDirectory = File.join(@libFolder, name)
		FileUtils.mkdir_p(targetDirectory) unless File.directory?(targetDirectory)
		source = File.join(@pacFolder, name, version, file)
		destination = File.join(@libFolder, name, file)
		FileUtils.cp(source, destination)	
	end

	def copyDependency (name, version, subFolder=name)
		source = File.join(@pacFolder, name, version)
    source = File.join(source, '\.') unless source =~ /\\\.$/
		destination = File.join(@libFolder, subFolder)
    puts source
		FileUtils.cp_r(source, destination)
	end
	
	def PullPac
		pacRepository = SourceControlRepository.new(@pacFolder)
		pacRepository.PullAndFastForward()
	end
	
	def CleanLib
		lib = SourceControlRepository.new(@libFolder)
		lib.Clean()	
	end

	def pacExists()
		return File.directory?(@pacFolder)
	end

end

class PackageManager

	attr_accessor :sourcePath, :artifactsPath, :environment, :zipFile, :zip
	attr_accessor :configFileName
	
	def initialize()
		@deleteOriginalConfig = false
		@zip = ZipDirectory.new()
	end
	
	def BuildConfig()
		puts "Building " + @environment + " config"
		original = File.join(@sourcePath, @configFileName)
		new = File.join(@artifactsPath, @configFileName)
		FileUtils.copy original, new
		text = File.read(new)
		modified = text.gsub(/.dev./, "." + @environment + ".")
		File.open(new, 'w') {|f| f.write(modified) }
	end
	
	def BuildZip()
		BuildConfig()
		puts "Building " + @zipFile
		@zip.directories_to_zip = [@sourcePath]
		@zip.output_path = @artifactsPath
		@zip.output_file = @zipFile
		@zip.execute()
		PutConfigFileInZip()
	end
	
	def PutConfigFileInZip()
		ZipFile.open(File.join(@artifactsPath, @zipFile)) { |z| 
			z.remove(@configFileName) unless !z.find_entry(@configFileName)
			z.add(@configFileName, File.join(@artifactsPath, @configFileName))
		}	
	end
end

task :default do
	puts('No default task, please choose from the following (rake -T):')
	system("rake -T")
end

desc "Clean the repository before a build"
task :clean do
    puts "Cleaning"
    FileUtils.rm_rf $artifactsPath
	bins = FileList[File.join($srcPath, "**/bin")].map{|f| File.expand_path(f)}
	bins.each do |file|
		sh %Q{rmdir /S /Q "#{file}"}
		#sh 
    end
end

desc "Build the solution for this project"
msbuild :build_release => [:clean] do |msb|
  msb.properties :configuration => :Release
  msb.targets :Build
  msb.solution = $projectSolution
end

class ServiceManager
	attr_accessor :serviceName, :host
	
	def initialize(serviceName, host)
		@serviceName = serviceName
		@host = host
	end
	
	def setRestartOnFailures
		puts 'setting service retry on failure ' + serviceName
		system("sc \\\\" + @host + " failure " + serviceName + " reset= 60 actions= restart/60000/restart/60000//")
	end
	
	def start
		puts 'starting ' + serviceName
		system("sc \\\\" + @host + " start " + serviceName)
	end
end


desc "Update PAC and copy dependencies locally"
task :pac do
	manager = get_pac_manager()
	if(ENV['Teamcity'] != nil) then
		puts "Skipping PAC update for teamcity"
	else
		manager.PullPac()
	end
	manager.CleanLib()
	copy_pac(manager)
end

def get_pac_manager
	manager = PacManager.new()
	puts "PAC located at " + manager.pacFolder
	if(!manager.pacExists()) then
		raise "PAC missing, cannot be found at " + manager.pacFolder
	end
	return manager
end

desc "Only copy the latest pac contents"
task :pac_copy do
	manager = get_pac_manager()
	manager.CleanLib()
	copy_pac(manager)
end