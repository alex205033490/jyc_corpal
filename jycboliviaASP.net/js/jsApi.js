//-------------------------------   FUNCION AGREGAR FILAS FORM

function deleteRow(button) {
    var row = button.parentNode.parentNode;
    row.parentNode.removeChild(row);
}


// . a ,
function convertdotcomma(event) {
    var input = event.target;

    input.value = input.value.replace('.',',');
}




