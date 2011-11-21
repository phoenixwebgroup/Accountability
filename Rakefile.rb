require 'rake'
require 'albacore'

$projectSolution = 'src/Accountability.sln'
$artifactsPath = "build"
$nugetFeedPath = ENV["NuGetDevFeed"]
$srcPath = File.expand_path('src')

task :teamcity => [:build_release]

task :build => [:build_release]

msbuild :build_release => [:clean, :dep] do |msb|
  msb.properties :configuration => :Release
  msb.targets :Build
  msb.solution = $projectSolution
end

task :clean do
    puts "Cleaning"
    FileUtils.rm_rf $artifactsPath
	bins = FileList[File.join($srcPath, "**/bin")].map{|f| File.expand_path(f)}
	bins.each do |file|
		sh %Q{rmdir /S /Q "#{file}"}
    end
end

desc "Setup dependencies for nuget packages"
task :dep do
	package_folder = File.expand_path('src/Packages')
    packages = FileList["**/packages.config"].map{|f| File.expand_path(f)}
	packages.each do |file|
		sh %Q{nuget install #{file} /OutputDirectory #{package_folder}}
    end
end

desc "Backup mongo database"
task :mongobackup do
	sh "mongodump -d Accountability -o mongobackup"
end