# MySQL Maestro
#
# SQL Editor queries 

# Query "Query &1"

///---------------------update_callcenter----------------

update tb_evento set tb_evento.areacallcenter = 0,
tb_evento.arearin = 0,
tb_evento.arearcc = 0,
tb_evento.areacliente = 0;


update tb_evento
set
tb_evento.areacallcenter = 1,
tb_evento.arearin = 0,
tb_evento.arearcc = 0,
tb_evento.areacliente = 0 
where
tb_evento.cambiorepuesto = 0 and                               
(tb_evento.arearcc is null or tb_evento.arearcc<>1) and
(tb_evento.arearin is null or tb_evento.arearin<>1) and 
(tb_evento.areacliente is null or tb_evento.areacliente<>1);

--------------------update_rin
update tb_evento 
set tb_evento.arearin = 0;

update tb_evento 
set tb_evento.arearin = 1,
tb_evento.areacallcenter = 0,
tb_evento.arearcc = 0,
tb_evento.areacliente = 0 
where
tb_evento.cambiorepuesto = 1 and 
(tb_evento.solicitudRepuesto is null or tb_evento.aceptacionproforma is not null) and
(tb_evento.areacliente is null or tb_evento.areacliente<>1) and
(tb_evento.arearcc is null or tb_evento.arearcc<>1) and
(tb_evento.areacallcenter is null or tb_evento.areacallcenter<>1) ; 


//-------------------update RCC--------------

update tb_evento 
set tb_evento.arearcc = 0;

update tb_evento 
set tb_evento.arearcc = 1,
tb_evento.areacallcenter = 0,
tb_evento.arearin = 0,
tb_evento.areacliente = 0 
where
tb_evento.cambiorepuesto = 1 and 
tb_evento.solicitudrepuestobandera = 1 and
tb_evento.solicitudRepuesto is not null and
tb_evento.aceptacionproforma is null and
(tb_evento.areacliente is null or tb_evento.areacliente<>1) and
(tb_evento.arearin is null or tb_evento.arearin<>1) and
(tb_evento.areacallcenter is null or tb_evento.areacallcenter<>1); 
 



//----------------------update areacliente
update tb_evento 
set tb_evento.areacotirepuesto = 0;

update tb_evento set
tb_evento.arearin = 0,
tb_evento.arearcc = 0,
tb_evento.areacallcenter = 0,
tb_evento.areacliente = 1,
tb_evento.areacotirepuesto = 1
where
tb_evento.aceptacionproforma is null and
tb_evento.cambiorepuesto = 1 and
tb_evento.codigo in 
(
select dd.codcallcenter from 
tb_detalle_callcenter_cotirepuesto dd, tb_cotizacionrepuesto cc
where 
dd.codcotirepuesto = cc.codigo and
cc.cite is not null
group by
dd.codcallcenter
);




///-------------------select callcenter

select even.codigo as Ticket, 
even.semana,
even.hora, 
date_format(even.fecha,'%d/%m/%y') as Fecha, 
even.cliente, 
even.nombreEdificio, even.ascensorparado as 'Ascensor Parado',even.personasatrapadas as 'Personas Atrapadas' ,
tv.nombre as 'TipoEvento', 
p.nombre as 'Prioridad ', 
resp.nombre as 'EventoAbierto' 
,even.Observacion as 'Solicitud de Servicio o Atencion'
,even.observacion_evento as 'Observacion_Cierre_Evento'
from tb_evento even, tb_tipoevento tv, tb_prioridad p, tb_responsable resp 
where even.estadoEvento = 'Abierto' and 
even.cambiorepuesto = 0 and                               
even.prioridad = p.codigo and 
even.codtipoevento = tv.codigo and 
even.inicioeventouser = resp.codigo and
even.areacallcenter = 1 and
(even.arearcc is null or even.arearcc<>1) and
(even.arearin is null or even.arearin<>1) and 
(even.areacliente is null or even.areacliente<>1)

//--------------------select RIN
select even.codigo as Ticket, even.semana, even.hora, date_format(even.fecha,'%d/%m/%y') as Fecha, even.cliente, 
even.nombreEdificio, even.ascensorparado as 'Ascensor Parado',even.personasatrapadas as 'Personas Atrapadas' ,
tv.nombre as 'TipoEvento', 
p.nombre as 'Prioridad ' 
,even.Observacion as 'Solicitud de Servicio o Atencion'
, even.observacion_evento, 
DATE_FORMAT(even.solicitudRepuesto,'%d/%m/%Y') as 'Solicitud_Repuesto', 
DATE_FORMAT(even.envioproforma,'%d/%m/%Y') as 'Envio_Proforma', 
DATE_FORMAT(even.aceptacionproforma,'%d/%m/%Y') as 'Aceptacion_Proforma', 
even.verificacion_cambio, 
even.observacion_necesidadrepuesto 
from tb_evento even, tb_tipoevento tv, tb_prioridad p 
where even.estadoEvento = 'Abierto' and 
even.cambiorepuesto = 1 and 
even.solicitudrepuestobandera = 0 and 
even.prioridad = p.codigo and 
even.codtipoevento = tv.codigo and
even.arearin = 1 and
(even.solicitudRepuesto is null or even.aceptacionproforma is not null) and
(even.areacliente is null or even.areacliente<>1) and
(even.arearcc is null or even.arearcc<>1) and
(even.areacallcenter is null or even.areacallcenter<>1) and 
 

-----select RCC 
 
select even.codigo as Ticket, even.semana, even.hora, date_format(even.fecha,'%d/%m/%y') as Fecha, even.cliente, 
even.nombreEdificio, even.ascensorparado as 'Ascensor Parado',even.personasatrapadas as 'Personas Atrapadas' ,
tv.nombre as 'TipoEvento', 
p.nombre as 'Prioridad ' 
,even.Observacion as 'Solicitud de Servicio o Atencion'
, even.observacion_evento, 
DATE_FORMAT(even.solicitudRepuesto,'%d/%m/%Y') as 'Solicitud_Repuesto', 
DATE_FORMAT(even.envioproforma,'%d/%m/%Y') as 'Envio_Proforma', 
DATE_FORMAT(even.aceptacionproforma,'%d/%m/%Y') as 'Aceptacion_Proforma', 
even.verificacion_cambio, 
even.observacion_necesidadrepuesto 
from tb_evento even, tb_tipoevento tv, tb_prioridad p 
where even.estadoEvento = 'Abierto' and 
even.cambiorepuesto = 1 and 
even.solicitudrepuestobandera = 1 and 
even.prioridad = p.codigo and 
even.codtipoevento = tv.codigo and
even.arearcc = 1 and
even.solicitudRepuesto is not null and
even.aceptacionproforma is null and
(even.areacliente is null or even.areacliente<>1) and
(even.arearin is null or even.arearin<>1) and
(even.areacallcenter is null or even.areacallcenter<>1) and 
 

//-------------------------select areacliente

select even.codigo as Ticket, even.semana, even.hora, date_format(even.fecha,'%d/%m/%y') as Fecha, even.cliente, 
even.nombreEdificio, even.ascensorparado as 'Ascensor Parado',even.personasatrapadas as 'Personas Atrapadas' ,
tv.nombre as 'TipoEvento', 
p.nombre as 'Prioridad ' 
,even.Observacion as 'Solicitud de Servicio o Atencion'
, even.observacion_evento, 
DATE_FORMAT(even.solicitudRepuesto,'%d/%m/%Y') as 'Solicitud_Repuesto', 
DATE_FORMAT(even.envioproforma,'%d/%m/%Y') as 'Envio_Proforma', 
DATE_FORMAT(even.aceptacionproforma,'%d/%m/%Y') as 'Aceptacion_Proforma', 
even.verificacion_cambio, 
even.observacion_necesidadrepuesto 
from tb_evento even, tb_tipoevento tv, tb_prioridad p 
where even.estadoEvento = 'Abierto' and 
even.prioridad = p.codigo and 
even.codtipoevento = tv.codigo and
even.areacliente = 1 and
(even.arearcc is null or even.arearcc<>1) and
(even.arearin is null or even.arearin<>1) and
(even.areacallcenter is null or even.areacallcenter<>1)
//---------------------------------------------------------
//---------------------------------------------------------
//---------------------------------------------------------

select  
ru.codigo as 'NroRuta', 
cc.diasemana, 
cc.horaentrada as 'Hora_Entrada', 
cc.horasalida as 'Hora_Salida', 
timediff(cc.horasalida,cc.horaentrada) as 'Total_Hora', 
proy.nombre as 'Edificio', 
eq.exbo, 
cc.nrovisita,  
cc.nrodia, 
date_format(cc.fechas1, '%d/%m/%Y')  as 'Semana1', 
date_format(cc.fechas2, '%d/%m/%Y')  as 'Semana2', 
date_format(cc.fechas3, '%d/%m/%Y')  as 'Semana3', 
date_format(cc.fechas4, '%d/%m/%Y')  as 'Semana4'
from  
tb_ruta ru, 
tb_proyecto proy, 
tb_equipo eq,   
tb_cronogramavisitarutamanteminieto cc  
where  
eq.codigo = cc.codeq and
ru.codigo = cc.codruta and
eq.cod_proyecto = proy.codigo and 
cc.codmes = 9 and cc.anio = 2017
order by ru.codigo,cc.nrodia,cc.horaentrada asc

# Query "Query &2"



# Query "Query &3"



