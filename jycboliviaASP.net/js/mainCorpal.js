$(document).ready(function () {
    var gridViewId = $(".container_listRegistros").data("clientid");

    $("#" + gridViewId + " input[type='checkbox']").change(function () {
        var row = $(this).closest("tr");

        if ($(this).is(":checked")) {
            row.addClass("highlighted");
        } else {
            row.removeClass("highlighted");
        }
    });
});
