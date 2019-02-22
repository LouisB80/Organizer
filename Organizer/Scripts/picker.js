$(function () {
    $('#DateApp').datetimepicker(
        {
            format: 'd/m/Y H:i',
            lang: 'fr',
            mask: true
        }
    );
    $('#delModal').modal('show');
});