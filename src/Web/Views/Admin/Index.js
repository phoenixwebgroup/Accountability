$(function () {

	function itemSaved() {
		$('#editor').html('Saved');
		$('#FilterForm').submit();
	}

	function loadEditor(data) {
		$('#editor').html(data);
		$('#editor form').ajaxForm({
			success: function () {
				itemSaved();
			}
		});
	}

	$('#AddMetric').click(function () {
		$.ajax({
			url: 'Admin/AddMetric',
			success: loadEditor
		});
	});

	$('#AddSource').click(function () {
		$.ajax({
			url: 'Admin/AddSource',
			success: loadEditor
		});
	});
});