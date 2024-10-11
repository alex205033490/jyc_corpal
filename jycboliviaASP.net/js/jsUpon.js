function addRowEgreso() {
    var table = document.getElementById("tblDetalleProductosEgresos");
    var rowCount = table.rows.length;
    var row = table.insertRow(rowCount);
    row.innerHTML =
        `<td><input type='number' class="inputNumber" name='item${rowCount}' /></td>
         <td><input type='text' class="inputText" name='codigoProducto${rowCount}' /></td>
         <td><input type='number' class="inputNumber" name='unidadMedida${rowCount}' /></td>
         <td><input type='number' class="inputNumber" name='cantidad${rowCount}' /></td>`;
}

function addRowIngreso() {
    var table = document.getElementById("tblDetalleProductosIngresos");
    var rowCount = table.rows.length;
    var row = table.insertRow(rowCount);
    row.innerHTML =
            `<td><input type='text' class="inputText" name='codigoProducto${rowCount}' /></td>
            <td><input type='number' class="inputNumber" name='unidadMedida${rowCount}' /></td>
            <td><input type='number' class="inputNumber" name='cantidad${rowCount}' /></td>
            <td><input type='number' class="inputNumber" name='costoUnitario${rowCount}' /></td>`;
}
function addRowProduccion() {
    var table = document.getElementById("tblDetalleProductoProduccion");
    var rowCount = table.rows.length;
    var row = table.insertRow(rowCount);
    row.innerHTML =
        `<td><input type='number' class="inputNumber" name='item${rowCount}' /></td>
         <td><input class="inputText" type='text' name='codigoProducto${rowCount}' /></td>
         <td><input class="inputNumber" type='number' name='cantidad${rowCount}' /></td>
         <td><input class="inputNumber" type='number' name='unidadMedida${rowCount}' /></td>
         <td><input class="inputText" type='text' name='codigoReceta${rowCount}' /></td>`;
}
function addRowPedido() {
    var table = document.getElementById("tblDetalleProductosPedido");
    var rowCount = table.rows.length;
    var row = table.insertRow(rowCount);
    row.innerHTML =
        `<td><input type='number' class="inputNumber" name='numeroItem${rowCount}' /></td>
         <td><input class="inputText" type='text' name='codigoProducto${rowCount}' /></td>
         <td><input class="inputNumber" type='number' name='cantidad${rowCount}' /></td>
         <td><input class="inputNumber" type='number' name='codigoUnidadMedida${rowCount}' /></td>
         <td><input class="inputNumber" type='number' name='precioUnitario${rowCount}' /></td>
         <td><input class="inputNumber" type='number' name='importeDescuento${rowCount}' /></td>
         <td><input class="inputNumber" type='number' name='importeTotal${rowCount}' /></td>`;
}