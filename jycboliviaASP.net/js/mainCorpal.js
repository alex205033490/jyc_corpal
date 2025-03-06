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


$(document).ready(function () {
    function addCheckboxChangeListener() {
        var gridViewId = $(".container-gvRegistros").data("clientid");

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


