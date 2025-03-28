$(document).ready(function () {
    function addCheckboxChangeListener(){
        var gridViewId = $(".container_listRegistros").data("clientid");

        $("#" + gridViewId + " input[type='checkbox']").change(function () {
            var row = $(this).closest("tr");

            if ($(this).is(":checked")) {
                row.addClass("highlighted");
            } else {
                row.removeClass("highlighted");
            }
        });
    }
    addCheckboxChangeListener();
    Sys.Application.add_load(function () {
        addCheckboxChangeListener();
    });
});





/*   JS FCorpal_GestionExtintores   */
$(document).ready(function () {
    var table = $(".table-sticky");

    if (table.find("thead").length === 0) {
        table.prepend("<thead>" + table.find("tr:first").html() + "</thead>");
        table.find("tr:first").remove();
    }

})

function convertdotcomma(event) {
    var input = event.target;

    input.value = input.value.replace('.', ',');
}











