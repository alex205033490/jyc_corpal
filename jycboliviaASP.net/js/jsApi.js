//-------------------------------   FUNCION AGREGAR FILAS FORM
function addRowIngreso() {
    var table = document.getElementById("tblDetalleProductosIngresos");
    var rowCount = table.rows.length;
    var row = table.insertRow(rowCount);
      
    row.innerHTML = `
        <td><input type='text' class="form-control" name='codigoProducto${rowCount}' id='codigoProducto${rowCount}' autocomplete="off" /></td>
        <td><input type='number' class="form-control" name='unidadMedida${rowCount}' id='unidadMedida${rowCount}' /></td>
        <td><input type='number' class="form-control" step="0.01" name='cantidad${rowCount}' id='cantidad${rowCount}' /></td>
        <td><input type='number' class="form-control" step="0.01" name='costoUnitario${rowCount}' id='costoUnitario${rowCount}' /></td>
        <td><button type="button" class="btn btn-danger" onclick="deleteRow(this)">Eliminar</button></td>
    `;
}   

function deleteRow(button) {
    var row = button.parentNode.parentNode;  
    row.parentNode.removeChild(row);  
}



/*function addRowIngreso() {
    var table = document.getElementById("tblDetalleProductosIngresos");
    var rowCount = table.rows.length;
    var row = table.insertRow(rowCount);
    row.innerHTML =
        `
    <td><input type='text' class="form-control" name='codigoProducto${rowCount}' /></td>
    <td><input type='number' class="form-control" name='unidadMedida${rowCount}' /></td>
    <td><input type='number' class="form-control" step="0.01" name='cantidad${rowCount}' /></td>
    <td><input type='number' class="form-control" step="0.01" name='costoUnitario${rowCount}' /></td>`;
}   */

function addRowEgreso() {
    var table = document.getElementById("tblDetalleProductosEgresos");
    var rowCount = table.rows.length;
    var row = table.insertRow(rowCount);
    row.innerHTML =
        `<td><input type='text' class="form-control" name='codigoProducto${rowCount}' /></td>
         <td><input type='number' class="form-control" name='unidadMedida${rowCount}' /></td>
         <td><input type='number' class="form-control" step="0.01" name='cantidad${rowCount}' /></td>`;
}

function addRowProduccion() {
    var table = document.getElementById("tblDetalleProductoProduccion");
    var rowCount = table.rows.length;
    var row = table.insertRow(rowCount);
    row.innerHTML =
        `<td><input class="inputText" type='text' name='codigoProducto${rowCount}' /></td>
        <td><input class="inputNumber" type='number' name='cantidad${rowCount}' /></td>
        <td><input class="inputNumber" type='number' name='unidadMedida${rowCount}' /></td>
        <td><input class="inputText" type='text' name='codigoReceta${rowCount}' /></td>`;
}

function addRowPedido() {
    var table = document.getElementById("tblDetalleProductosPedido");
    var rowCount = table.rows.length;
    var row = table.insertRow(rowCount);
    row.innerHTML =
        `<td><input class="form-control" type='text' name='codigoProducto${rowCount}' /></td>
         <td><input class="form-control" type='number' step="0.01" name='cantidad${rowCount}' /></td>
         <td><input class="form-control" type='number' name='codigoUnidadMedida${rowCount}' /></td>
         <td><input class="form-control" type='number' step="0.01" name='precioUnitario${rowCount}' /></td>
         <td><input class="form-control" type='number' step="0.01" name='importeDescuento${rowCount}' /></td>`;
}

function addRowCobranza() {
    var table = document.getElementById("tblDetalleCuentas");
    var rowCount = table.rows.length;
    var row = table.insertRow(rowCount);
    row.innerHTML =
        `<td><input type='number' name='numeroCuenta${rowCount}' /></td>
        <td><input type='number' name='importeCapital${rowCount}' /></td>`;
}

