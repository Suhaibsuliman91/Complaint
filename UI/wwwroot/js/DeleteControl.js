$('.Delete').on('click', function () {
    //$('#exampleModal').modal('show');
    //$('#exampleModal').modal('hide');

    var id = parseInt($(this).attr('data-value'));
    var Controller = $(this).attr('data-id');
    var requestUrl = Controller + '/Delete'
    var element = $(this).parent().parent();
    //debugger;
    $.ajax({
        url: requestUrl,
        method: 'DELETE',
        data: {
            ID: id
        },
        success: function (result) {
            //debugger;
            if (result.id > -99) {
                //debugger;
                $('#IndexTable tr').remove('.Row_' + result.id);
            } else {
                console.log("failed To Delete");
            }
        }

    })
});