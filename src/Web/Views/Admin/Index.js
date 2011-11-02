$(function () {

	$('#AddMetric').click(function () {
		$.ajax({
			url: 'Admin/AddMetric',
			success: loadEditor
		});
	});

	$('#AddUser').click(function () {
		$.ajax({
			url: 'Admin/AddUser',
			success: loadEditor
		});
	});

});

function itemSaved(data) {
	$('#editor').html(data);
	$('#FilterForm').submit();
}

function loadEditor(data) {
	$('#editor').html(data);
	$('#editor form').ajaxForm({
		success: function (data) {
			itemSaved(data);
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