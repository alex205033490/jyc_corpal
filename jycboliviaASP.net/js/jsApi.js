//-------------------------------   FUNCION AGREGAR FILAS FORM
function addRowIngreso() {
    var table = document.getElementById("tblDetalleProductosIngresos");
    var rowCount = table.rows.length;
    var row = table.insertRow(rowCount);
    row.innerHTML =
        `
    <td><input type='text' class="form-control" name='codigoProducto${rowCount}' /></td>
    <td><input type='number' class="form-control" name='unidadMedida${rowCount}' /></td>
    <td><input type='number' class="form-control" step="0.01" name='cantidad${rowCount}' /></td>
    <td><input type='number' class="form-control" step="0.01" name='costoUnitario${rowCount}' /></td>`;
}


function addRowEgreso() {
    var table = document.getElementById("tblDetalleProductosEgresos");
    var rowCount = table.rows.length;
    var row = table.insertRow(rowCount);
    row.innerHTML =
        `<td><input type='text' class="form-control" name='codigoProducto${rowCount}' /></td>
         <td><input type='number' class="form-control" name='unidadMedida${rowCount}' /></td>
         <td><input type='number' class="form-control" step="0.01" name='cantidad${rowCount}' /></td>`;
}