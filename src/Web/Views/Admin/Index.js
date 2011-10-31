$(function () {

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

function editSelected() {
	var data = $.pageActions.GetSelectedRecordData();
	$.ajax({
		url: 'Admin/Edit',
		data: data,
		success: loadEditor
	});
}