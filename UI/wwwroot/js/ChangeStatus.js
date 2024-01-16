$(".Approve").on("click", function () {
    //debugger;
    var element = $(this);
    var ID = $(element).parent().parent().parent().parent().attr("id").split('_');
    //console.log(ID);
    $.ajax({
        url: '/Complaint/ChangeStatus',
        data: {
            ID: ID[1],
            StatusID: 2
        },
        method: 'POST',
        type: 'JSON',
        success: function (result) {
            //debugger;
            if (result.isSuccess)
            {
                var temp = $(element).parent().parent().parent().parent().children();
                temp[5].innerText = "Approved";
            }
            //alert("Approved");
        }

    });
});
$(".Reject").on("click", function () {
    var element = $(this);
    var ID = $(element).parent().parent().parent().parent().attr("id").split('_');
    //console.log(ID);
    $.ajax({
        url: '/Complaint/ChangeStatus',
        data: {
            ID: ID[1],
            StatusID: 3
        },
        method: 'POST',
        type: 'JSON',
        success: function (result) {
            if (result.isSuccess) {
                var temp = $(element).parent().parent().parent().parent().children();
                temp[5].innerText = "Rejected";
            }
        }
    });
});