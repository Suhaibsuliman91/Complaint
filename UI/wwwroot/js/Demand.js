$("#AddDemand").on("click", function () {
    debugger;
    var DescriptionEnglish = $("#Description_en").val();
    var DescriptionArabic = $("#Description_ar").val();
    var RowNum = $("#DemandTabel tr").length;

    var Demand = {
        "DescriptionEn": DescriptionEnglish,
        "DescriptionAR": DescriptionArabic,
        "ID": RowNum,
        "ComplaintID": 0,
        "IsDeleted": false
    }

    $.ajax({
        url: '/Complaint/Demand',
        data: {
            demand: Demand
        },
        method: 'POST',
        type: 'JSON',
        success: function (result) {
            $("#DemandTabel").append(result);
        }

    });

});