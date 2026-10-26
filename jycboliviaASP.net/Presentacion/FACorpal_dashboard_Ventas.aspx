<%@ Page Title="Dashboard de ventas" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FACorpal_dashboard_Ventas.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FACorpal_dashboard_Ventas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Tipografías del tablero y librería para Excel --%>
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Barlow+Semi+Condensed:wght@500;600;700&family=Source+Sans+3:wght@400;600;700&display=swap" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.18.5/xlsx.full.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- Inyección de datos desde C# --%>
    <script>
        window.DV_DATOS = <%= DatosJsonParaFrontend %>;
        window.DV_OBJETIVOS = <%= ObjetivosJsonParaFrontend %>;
        window.DV_META = {
            fuente: "Base de Datos MySQL (Corpal)",
            filas: <%= TotalFilasOrigen %>,
            totalOrigen: <%= TotalOrigenGeneral.ToString(System.Globalization.CultureInfo.InvariantCulture) %> 
        };
    </script>

    <%-- Contenedor del Shadow DOM --%>
    <div id="dvHost"></div>

    <template id="dvTpl">
<style>
:host{
  --bg:#f3f5f8; --panel:#ffffff; --fg:#17202c; --muted:#5d6a7b; --line:#dfe4eb; --soft:#eef1f6;
  --vend:#e2662c; --prod:#1d7ae0; --canal:#0f9b8e; --y1:#9fb4cc; --y2:#1d4f91; --accent:#1d4f91;
  --sel-bg:#17202c; --sel-fg:#ffffff; --track:#edf0f4; --warn:#b26a00; --ok:#1d7a46; --bad:#c0392b;
  --shadow:0 1px 2px rgba(23,32,44,.06),0 2px 8px rgba(23,32,44,.05);
  --f-disp:"Barlow Semi Condensed","Arial Narrow",Arial,sans-serif;
  --f-body:"Source Sans 3","Segoe UI",Roboto,Arial,sans-serif;
}
.wrap{max-width:1680px;margin:0 auto;padding-block:14px 40px;padding-left:max(16px,env(safe-area-inset-left,0px));padding-right:max(16px,env(safe-area-inset-right,0px));display:flex;flex-direction:column;gap:14px}
.num{font-variant-numeric:tabular-nums}
button{font:inherit;color:inherit;cursor:pointer}
button:focus-visible,input:focus-visible,.row:focus-visible{outline:2px solid var(--accent);outline-offset:2px}
header.top{display:flex;flex-wrap:wrap;align-items:flex-end;justify-content:space-between;gap:12px}
.brand small{display:block;font-size:12px;letter-spacing:.12em;text-transform:uppercase;color:var(--muted);font-weight:600}
.brand h1{margin:0;font-family:var(--f-disp);font-weight:700;font-size:30px;line-height:1.05;letter-spacing:.01em}
.brand .src{font-size:13px;color:var(--muted);margin-top:4px}
.actions{display:flex;flex-wrap:wrap;gap:8px;align-items:center}
.btn{border:1px solid var(--line);background:var(--panel);padding:7px 12px;border-radius:6px;font-weight:600;font-size:13px;display:inline-flex;gap:6px;align-items:center}
.btn:hover{border-color:var(--muted)}
.btn.primary{background:var(--sel-bg);color:var(--sel-fg);border-color:var(--sel-bg)}
.panel{background:var(--panel);border:1px solid var(--line);border-radius:8px;box-shadow:var(--shadow);min-width:0}
.ph{display:flex;justify-content:space-between;align-items:baseline;gap:8px;padding:12px 14px 6px;flex-wrap:wrap}
.ph h2{margin:0;font-family:var(--f-disp);font-weight:600;font-size:18px;letter-spacing:.01em}
.ph .hint{font-size:12px;color:var(--muted)}
/* Slicers */
.slicers{display:grid;grid-template-columns:auto minmax(260px,1.3fr) repeat(2,minmax(170px,1fr));gap:14px;padding:12px 14px;align-items:start}
.sl-label{font-size:11px;letter-spacing:.1em;text-transform:uppercase;color:var(--muted);font-weight:700;margin-bottom:6px;display:flex;justify-content:space-between;gap:6px}
.years{display:flex;flex-direction:column;gap:6px}
.tile{border:1px solid var(--line);background:var(--panel);border-radius:5px;padding:6px 10px;text-align:left;transition:background .12s}
.tile:hover{border-color:var(--muted)}
.tile.on{background:var(--sel-bg);color:var(--sel-fg);border-color:var(--sel-bg)}
.tile.zero{opacity:.45}
.tile b{font-family:var(--f-disp);font-size:18px;font-weight:600;display:block;line-height:1.1}
.tile span{font-size:11px;opacity:.8}
.months{display:grid;grid-template-columns:repeat(6,1fr);gap:5px}
.months .tile{padding:5px 4px;text-align:center}
.months .tile b{font-size:16px}
.dd{position:relative}
.dd>button{width:100%;text-align:left;border:1px solid var(--line);background:var(--panel);border-radius:5px;padding:8px 30px 8px 10px;position:relative;white-space:nowrap;overflow:hidden;text-overflow:ellipsis}
.dd>button::after{content:"";position:absolute;right:11px;top:50%;width:7px;height:7px;border-right:2px solid var(--muted);border-bottom:2px solid var(--muted);transform:translateY(-70%) rotate(45deg)}
.dd>button.has{border-color:var(--sel-bg);font-weight:600}
.ddp{position:absolute;z-index:30;top:calc(100% + 4px);left:0;right:0;min-width:260px;background:var(--panel);border:1px solid var(--line);border-radius:6px;box-shadow:0 8px 24px rgba(0,0,0,.18);padding:8px}
.ddp input{width:100%;box-sizing:border-box;padding:6px 8px;border:1px solid var(--line);border-radius:4px;background:var(--bg);color:var(--fg);font:inherit;margin-bottom:6px}
.ddp ul{list-style:none;margin:0;padding:0;max-height:280px;overflow:auto}
.ddp li label{display:flex;gap:8px;padding:4px;border-radius:4px;cursor:pointer;font-size:13px;align-items:flex-start}
.ddp li label:hover{background:var(--soft)}
.chips{display:flex;flex-wrap:wrap;gap:6px;align-items:center;padding:0 14px 12px;min-height:8px}
.chips:empty{display:none}
.chip{display:inline-flex;align-items:center;gap:6px;background:var(--soft);border:1px solid var(--line);border-radius:999px;padding:3px 4px 3px 10px;font-size:12.5px}
.chip button{border:0;background:var(--line);width:20px;height:20px;border-radius:50%;line-height:20px;padding:0;font-size:14px}
.chip button:hover{background:var(--bad);color:#fff}
/* KPI */
.kpis{display:grid;grid-template-columns:2.1fr repeat(4,1fr);gap:12px}
.kpi{padding:12px 14px;display:flex;flex-direction:column;gap:3px;min-width:0}
.kpi .k{font-size:11px;letter-spacing:.1em;text-transform:uppercase;color:var(--muted);font-weight:700}
.kpi .v{font-family:var(--f-disp);font-weight:700;font-size:26px;line-height:1.1;overflow-wrap:anywhere}
.kpi.main .v{font-size:clamp(26px,2.3vw,36px);white-space:nowrap}
.kpi .s{font-size:12.5px;color:var(--muted);overflow-wrap:anywhere}
.kpi .name{font-family:var(--f-disp);font-weight:600;font-size:17px;line-height:1.15;overflow-wrap:anywhere}
.delta{font-weight:700}
.delta.up{color:var(--ok)} .delta.down{color:var(--bad)}
/* Grid / Bars */
.grid{display:grid;grid-template-columns:repeat(12,minmax(0,1fr));gap:14px}
.c6{grid-column:span 6}.c12{grid-column:span 12}
.bars{padding:4px 14px 14px;display:flex;flex-direction:column;gap:2px}
.scroll{max-height:360px;overflow:auto}
.row{display:grid;grid-template-columns:minmax(120px,38%) 1fr;gap:10px;align-items:center;padding:4px 6px;border-radius:5px;cursor:pointer;border:1px solid transparent}
.row:hover{background:var(--soft)}
.row .nm{font-size:12.5px;line-height:1.2;text-align:right;overflow-wrap:anywhere}
.row .nm code{font-size:10.5px;color:var(--muted);display:block;font-family:inherit}
.row .tr{position:relative;display:flex;align-items:center;gap:8px;min-width:0}
.row .fill{height:18px;border-radius:2px;min-width:2px;flex:none}
.row .lb{font-size:12px;white-space:nowrap}
.row .lb i{font-style:normal;color:var(--muted)}
.row.on{border-color:var(--sel-bg);background:var(--soft)}
.row.on .nm{font-weight:700}
.dim .row:not(.on){opacity:.38}
.pt{display:flex;gap:8px;align-items:center;padding:0 14px 6px}
.pt input{flex:1;min-width:0;padding:6px 9px;border:1px solid var(--line);border-radius:5px;background:var(--bg);color:var(--fg);font:inherit}
.empty{padding:20px;color:var(--muted);text-align:center}
.chart{padding:4px 10px 10px;position:relative}
.chart svg{display:block;width:100%;height:auto}
.legend{display:flex;gap:14px;font-size:12px;color:var(--muted);align-items:center}
.legend i{display:inline-block;width:12px;height:12px;border-radius:2px;margin-right:5px;vertical-align:-2px}
svg .mbar{cursor:pointer}
svg text{fill:var(--fg)}
svg .ax{fill:var(--muted);font-size:11px}
svg .gl{stroke:var(--line)}
/* Table */
.tbar{display:flex;flex-wrap:wrap;gap:8px;align-items:center;padding:0 14px 8px}
.tbar input{flex:1 1 220px;min-width:0;padding:7px 9px;border:1px solid var(--line);border-radius:5px;background:var(--bg);color:var(--fg);font:inherit}
.tw{overflow-x:auto;max-height:450px;overflow-y:auto;border-top:1px solid var(--line)}
table{border-collapse:collapse;width:100%;font-size:12px;table-layout:fixed}
th,td{padding:7px 8px;border-bottom:1px solid var(--line);text-align:left;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;vertical-align:middle}
th{position:sticky;top:0;background:var(--soft);font-weight:700;cursor:pointer;user-select:none;font-size:11.5px;z-index:2}
th.r,td.r{text-align:right}
tbody tr:hover{background:var(--soft)}
tfoot td{font-weight:700;background:var(--soft)}
.pager{display:flex;flex-wrap:wrap;gap:8px;align-items:center;justify-content:space-between;padding:10px 14px;font-size:12.5px;color:var(--muted)}
.pager .pg{display:flex;gap:4px;align-items:center}
details.qa{padding:0}
details.qa summary{padding:12px 14px;cursor:pointer;font-family:var(--f-disp);font-weight:600;font-size:17px}
.qa ul{margin:0;padding:0 14px 14px 32px;display:flex;flex-direction:column;gap:6px;font-size:13px}
.qa li b{font-variant-numeric:tabular-nums}
.ok{color:var(--ok);font-weight:700}.bad{color:var(--bad);font-weight:700}
.notice{padding:10px 14px;border-radius:6px;border:1px solid var(--line);background:var(--panel);font-size:13px}
.notice.err{border-color:var(--bad);color:var(--bad)}
#tip{position:fixed;z-index:50;pointer-events:none;background:var(--panel);color:var(--fg);border:1px solid var(--line);box-shadow:0 6px 20px rgba(0,0,0,.2);border-radius:6px;padding:8px 10px;font-size:12.5px;max-width:300px;line-height:1.45}
#tip b{font-family:var(--f-disp);font-size:14px;display:block;margin-bottom:2px}
#tip .t2{display:grid;grid-template-columns:auto auto;gap:0 12px}
#tip .t2 span:nth-child(even){text-align:right;font-variant-numeric:tabular-nums}
:host{display:block;color:var(--fg);font-family:var(--f-body);font-size:14px;line-height:1.4}
*,*::before,*::after{box-sizing:border-box}
[hidden]{display:none!important}
.wrap{background:var(--bg);padding:14px 16px 28px;border-radius:8px;container-type:inline-size}
</style>
<div class="wrap">
  <header class="top">
    <div class="brand">
      <small>CORPAL S.R.L. · NAX SNAX</small>
      <h1>Tablero de ventas</h1>
      <div class="src" id="src">—</div>
    </div>
    <div class="actions">
      <button class="btn" id="multi" aria-pressed="false" title="Activado: cada clic agrega o quita elementos de la selección">Selección múltiple</button>
      <button class="btn primary" id="clear">Limpiar filtros</button>
    </div>
  </header>
  <div id="msg" hidden></div>

  <!-- Slicers / Filtros Superiores -->
  <section class="panel">
    <div class="slicers">
      <div><div class="sl-label">Año</div><div class="years" id="years"></div></div>
      <div><div class="sl-label">Mes <button id="mclr" hidden>quitar</button></div><div class="months" id="months"></div></div>
      <div><div class="sl-label">Vendedor</div><div class="dd" id="dd-v"></div></div>
      <div><div class="sl-label">Producto</div><div class="dd" id="dd-p"></div></div>
    </div>
    <div class="chips" id="chips"></div>
  </section>

  <!-- Tarjetas KPI -->
  <section class="kpis" id="kpis"></section>

  <!-- Gráficos y Tablas -->
  <section class="grid">
    <div class="panel c6">
      <div class="ph"><h2>Ventas por vendedor</h2><span class="hint">Clic para filtrar · Ctrl+clic suma a la selección</span></div>
      <div class="bars scroll" id="ch-v"></div>
    </div>
    <div class="panel c6">
      <div class="ph"><h2>Ventas por producto</h2><span class="hint" id="p-count"></span></div>
      <div class="pt"><input id="psearch" type="search" placeholder="Buscar producto o código…" aria-label="Buscar producto"></div>
      <div class="bars scroll" id="ch-p"></div>
    </div>
    <div class="panel c12">
      <div class="ph"><h2>Evolución mensual</h2><span class="legend" id="m-legend"></span></div>
      <div class="chart" id="ch-m"></div>
    </div>

    <!-- Tabla de Objetivos por Vendedor y Producto -->
    <div class="panel c12">
      <div class="ph">
        <h2>Cumplimiento de Objetivos por Vendedor y Producto</h2>
        <span class="hint">Desglose de metas de unidades por producto asignado vs. entregado</span>
      </div>
      <div class="tw scroll" style="max-height: 400px;">
        <table id="tbl-objetivos">
          <thead>
            <tr>
              <th>Vendedor</th>
              <th>Año / Mes</th>
              <th>Código</th>
              <th>Producto</th>
              <th class="r">Meta (Unid.)</th>
              <th class="r">Real (Unid.)</th>
              <th class="r">% Cumplimiento</th>
              <th style="text-align:center;">Estado</th>
            </tr>
          </thead>
          <tbody id="tbody-objetivos">
            <!-- Se llena dinámicamente -->
          </tbody>
        </table>
      </div>
    </div>

    <!-- Tabla de Detalle de Ventas -->
    <div class="panel c12">
      <div class="ph"><h2>Detalle de ventas</h2><span class="hint" id="t-info"></span></div>
      <div class="tbar">
        <input id="tsearch" type="search" placeholder="Buscar en el detalle: cliente, vendedor, producto, código, Nro. trans…" aria-label="Buscar en el detalle">
        <button class="btn" id="xlsx">Exportar Excel</button>
        <button class="btn" id="csv">Exportar CSV</button>
      </div>
      <div class="tw"><table id="tbl"><colgroup><col style="width:75px"><col style="width:55px"><col style="width:50px"><col style="width:75px"><col style="width:130px"><col style="width:75px"><col style="min-width:160px"><col style="width:65px"><col style="width:45px"><col style="width:65px"><col style="width:80px"><col style="min-width:140px"></colgroup><thead></thead><tbody></tbody><tfoot></tfoot></table></div>
      <div class="pager" id="pager"></div>
    </div>

    <details class="panel c12 qa" id="qa"><summary>Validación y calidad de datos</summary><ul id="qa-list"></ul></details>
  </section>
</div>
<div id="tip" hidden></div>
    </template>

    <script>
        (function () {
            "use strict";
            const HOST = document.getElementById("dvHost");
            const ROOT = HOST.shadowRoot || HOST.attachShadow({ mode: "open" });
            if (!ROOT.firstChild) ROOT.appendChild(document.getElementById("dvTpl").content.cloneNode(true));

            const MES = ["Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"];
            const MESL = ["enero", "febrero", "marzo", "abril", "mayo", "junio", "julio", "agosto", "septiembre", "octubre", "noviembre", "diciembre"];
            const $ = s => ROOT.querySelector(s);
            const esc = s => String(s).replace(/[&<>"']/g, c => ({ "&": "&amp;", "<": "&lt;", ">": "&gt;", '"': "&quot;", "'": "&#39;" }[c]));
            function fmt(n, dec) { const neg = n < 0; n = Math.abs(n); let s = n.toFixed(dec); let [i, d] = s.split("."); i = i.replace(/\B(?=(\d{3})+(?!\d))/g, "."); return (neg ? "-" : "") + i + (d ? "," + d : ""); }
            const bs = n => "Bs " + fmt(n, 2), bs0 = n => "Bs " + fmt(Math.round(n), 0), pct = n => fmt(n * 100, 1) + "%";
            const dstr = d => { const x = new Date(d * 864e5); return String(x.getUTCDate()).padStart(2, "0") + "/" + String(x.getUTCMonth() + 1).padStart(2, "0") + "/" + x.getUTCFullYear(); };

            let D, N, YR, MO, YEARS, maxDay;
            const S = { y: new Set(), m: new Set(), v: new Set(), p: new Set(), multi: false };
            let R = null;
            const T = { sort: "f", dir: -1, q: "", page: 0, size: 50 };

            function setData(d) {
                D = d; N = d.cols.t.length;
                YR = new Int16Array(N); MO = new Int8Array(N);
                const ys = new Set(); maxDay = -1e9;
                for (let i = 0; i < N; i++) { const x = new Date(d.cols.f[i] * 864e5); const y = x.getUTCFullYear(); ys.add(y); MO[i] = x.getUTCMonth() + 1; YR[i] = y; if (d.cols.f[i] > maxDay) maxDay = d.cols.f[i]; }
                YEARS = [...ys].sort((a, b) => a - b);
                const yi = {}; YEARS.forEach((y, i) => yi[y] = i); for (let i = 0; i < N; i++)YR[i] = yi[YR[i]];
                let mn = 1e9; for (const f of d.cols.f) if (f < mn) mn = f;
                d.meta.desde = mn; d.meta.hasta = maxDay;
                S.y.clear(); S.m.clear(); S.v.clear(); S.p.clear(); T.page = 0; T.q = ""; $("#tsearch").value = ""; $("#psearch").value = "";
                $("#src").textContent = "Datos del " + dstr(mn) + " al " + dstr(maxDay) + " · " + fmt(N, 0) + " líneas · fuente: " + d.meta.fuente;
                buildDropdowns(); renderQA(); update();
            }

            function renderObjetivosVendedores() {
                const tbody = ROOT.querySelector("#tbody-objetivos");
                if (!tbody) return;

                const objetivos = window.DV_OBJETIVOS || [];
                if (objetivos.length === 0) {
                    tbody.innerHTML = '<tr><td colspan="8" class="empty">No hay objetivos de productos registrados.</td></tr>';
                    return;
                }

                let filtrados = objetivos;
                if (S.v.size > 0) {
                    const vendedoresSeleccionados = [...S.v].map(idx => D.dims.v[idx]);
                    filtrados = objetivos.filter(item => vendedoresSeleccionados.includes(item.vendedor));
                }

                if (filtrados.length === 0) {
                    tbody.innerHTML = '<tr><td colspan="8" class="empty">No hay objetivos para el vendedor seleccionado.</td></tr>';
                    return;
                }

                let html = "";
                filtrados.forEach(item => {
                    const porc = item.PorcentajeCumplimiento || 0;
                    const estado = item.EstadoCumplimiento || "SIN OBJETIVO";

                    let badgeStyle = "background: var(--soft); color: var(--muted);";
                    if (estado === "CUMPLIÓ") {
                        badgeStyle = "background: rgba(29, 122, 70, 0.12); color: var(--ok); font-weight: 700;";
                    } else if (estado === "NO CUMPLIÓ") {
                        badgeStyle = "background: rgba(192, 57, 43, 0.12); color: var(--bad); font-weight: 700;";
                    }

                    html += `<tr>
              <td><b>${esc(item.vendedor)}</b></td>
              <td>${item.anio} - ${String(item.mes).padStart(2, '0')}</td>
              <td><code>${esc(item.CodProducto)}</code></td>
              <td>${esc(item.NombreProducto)}</td>
              <td class="r num">${fmt(item.MetaCantidadUnidades, 0)}</td>
              <td class="r num">${fmt(item.CantidadVendidaUnidades, 0)}</td>
              <td class="r num"><b>${fmt(porc, 1)}%</b></td>
              <td style="text-align:center;"><span style="padding: 3px 8px; border-radius: 4px; font-size: 11.5px; ${badgeStyle}">${estado}</span></td>
          </tr>`;
                });

                tbody.innerHTML = html;
            }

            function compute() {
                const c = D.cols, nY = YEARS.length, nV = D.dims.v.length, nP = D.dims.p.length;
                const act = [S.y.size > 0, S.m.size > 0, S.v.size > 0, S.p.size > 0];
                const sy = new Uint8Array(nY), sm = new Uint8Array(13), sv = new Uint8Array(nV), sp = new Uint8Array(nP);
                S.y.forEach(k => sy[k] = 1); S.m.forEach(k => sm[k] = 1); S.v.forEach(k => sv[k] = 1); S.p.forEach(k => sp[k] = 1);
                const A = (n) => ({ t: new Float64Array(n), q: new Float64Array(n), l: new Int32Array(n) });
                const chY = A(nY), chM = A(nY * 13), chV = A(nV), chP = A(nP);
                const fV = new Float64Array(nV), fP = new Float64Array(nP);
                const rows = []; let tot = 0, qty = 0, ncl = 0;
                let cmp = null;
                if (S.y.size === 1) {
                    const yi = [...S.y][0]; if (yi > 0) {
                        const yr = YEARS[yi]; let cut = null;
                        const last = new Date(maxDay * 864e5); if (last.getUTCFullYear() === yr) { cut = [last.getUTCMonth() + 1, last.getUTCDate()]; }
                        cmp = { yi: yi - 1, year: YEARS[yi - 1], cut, t: 0 };
                    }
                }
                const F = c.f, V = c.v, P = c.p, Tt = c.t, Q = c.q;
                for (let i = 0; i < N; i++) {
                    let fails = 0, fd = -1; const y = YR[i], m = MO[i], v = V[i], p = P[i];
                    if (act[0] && !sy[y]) { fails++; fd = 0; }
                    if (act[1] && !sm[m]) { fails++; fd = 1; }
                    if (act[2] && !sv[v]) { fails++; fd = 2; if (fails > 1) continue; }
                    if (act[3] && !sp[p]) { fails++; fd = 3; }
                    if (fails > 1) continue;
                    const t = Tt[i], q = Q[i];
                    if (fails === 0) {
                        tot += t; qty += q; rows.push(i); fV[v] += t; fP[p] += t; ncl++;
                        chY.t[y] += t; chY.q[y] += q; chY.l[y]++; const k = y * 13 + m; chM.t[k] += t; chM.q[k] += q; chM.l[k]++;
                        chV.t[v] += t; chV.q[v] += q; chV.l[v]++; chP.t[p] += t; chP.q[p] += q; chP.l[p]++;
                    } else if (fd === 0) {
                        chY.t[y] += t; chY.q[y] += q; chY.l[y]++; const k = y * 13 + m; chM.t[k] += t; chM.q[k] += q; chM.l[k]++;
                        if (cmp && y === cmp.yi) { if (!cmp.cut) cmp.t += t; else { const dd = new Date(F[i] * 864e5).getUTCDate(); if (m < cmp.cut[0] || (m === cmp.cut[0] && dd <= cmp.cut[1])) cmp.t += t; } }
                    }
                    else if (fd === 1) { const k = y * 13 + m; chM.t[k] += t; chM.q[k] += q; chM.l[k]++; }
                    else if (fd === 2) { chV.t[v] += t; chV.q[v] += q; chV.l[v]++; }
                    else if (fd === 3) { chP.t[p] += t; chP.q[p] += q; chP.l[p]++; }
                }
                const cnt = a => { let n = 0; for (const x of a) if (x > 0) n++; return n; };
                const top = a => { let bi = -1, bv = 0; for (let i = 0; i < a.length; i++)if (a[i] > bv) { bv = a[i]; bi = i; } return bi; };
                return { tot, qty, rows, ncl, chY, chM, chV, chP, fV, fP, nv: cnt(fV), np: cnt(fP), tv: top(fV), tp: top(fP), cmp };
            }

            function toggle(dim, key, ev) {
                const set = S[dim]; const add = S.multi || (ev && (ev.ctrlKey || ev.metaKey || ev.shiftKey));
                if (add) { set.has(key) ? set.delete(key) : set.add(key); }
                else if (set.size === 1 && set.has(key)) set.clear();
                else { set.clear(); set.add(key); }
                T.page = 0; update();
            }

            function update() {
                R = compute();
                renderSlicers();
                renderChips();
                renderKPI();
                renderBars("v");
                renderBars("p");
                renderMonthly();
                renderTable();
                renderObjetivosVendedores();
            }

            function renderSlicers() {
                const yEl = $("#years"); yEl.innerHTML = "";
                YEARS.forEach((y, i) => {
                    const b = document.createElement("button"); b.className = "tile num" + (S.y.has(i) ? " on" : "") + (R.chY.t[i] > 0 ? "" : " zero");
                    b.innerHTML = "<b>" + y + "</b><span>" + bs0(R.chY.t[i]) + "</span>"; b.setAttribute("aria-pressed", S.y.has(i));
                    b.onclick = e => toggle("y", i, e); tipOn(b, () => tipHTML(String(y), R.chY.t[i], R.chY.q[i], R.chY.l[i], null)); yEl.appendChild(b);
                });
                const mEl = $("#months"); mEl.innerHTML = "";
                for (let m = 1; m <= 12; m++) {
                    let t = 0; for (let y = 0; y < YEARS.length; y++)if (!S.y.size || S.y.has(y)) t += R.chM.t[y * 13 + m];
                    const b = document.createElement("button"); b.className = "tile num" + (S.m.has(m) ? " on" : "") + (t > 0 ? "" : " zero");
                    b.innerHTML = "<b>" + m + "</b><span>" + MES[m - 1] + "</span>"; b.setAttribute("aria-pressed", S.m.has(m)); b.setAttribute("aria-label", MESL[m - 1]);
                    b.onclick = e => toggle("m", m, e); tipOn(b, () => tipHTML(MESL[m - 1][0].toUpperCase() + MESL[m - 1].slice(1), t, null, null, null)); mEl.appendChild(b);
                }
                ["v", "p"].forEach(k => {
                    const b = $("#dd-" + k + ">button"); const n = S[k].size;
                    b.textContent = n === 0 ? "Todos" : n === 1 ? D.dims[k][[...S[k]][0]] : n + " seleccionados"; b.classList.toggle("has", n > 0); b.title = b.textContent;
                    const lst = $("#dd-" + k + " ul"); if (lst) lst.querySelectorAll("input").forEach(ch => ch.checked = S[k].has(+ch.value));
                });
            }

            function buildDropdowns() {
                ["v", "p"].forEach(k => {
                    const host = $("#dd-" + k); host.innerHTML = "";
                    const btn = document.createElement("button"); btn.type = "button"; btn.setAttribute("aria-haspopup", "listbox"); btn.id = "ddb-" + k; host.appendChild(btn);
                    const pan = document.createElement("div"); pan.className = "ddp"; pan.hidden = true;
                    pan.innerHTML = '<input type="search" placeholder="Buscar…" id="dds-' + k + '"><ul></ul><div class="ddf"><button class="btn" data-a="none">Quitar selección</button><button class="btn" data-a="close">Cerrar</button></div>';
                    host.appendChild(pan);
                    const ul = pan.querySelector("ul");
                    const order = D.dims[k].map((n, i) => i).sort((a, b) => D.dims[k][a].localeCompare(D.dims[k][b], "es"));
                    order.forEach(i => {
                        const li = document.createElement("li"); const code = k === "p" ? " <span style='color:var(--muted);font-size:11px'>" + esc(D.dims.pc[i]) + "</span>" : "";
                        li.innerHTML = '<label><input type="checkbox" value="' + i + '"><span>' + esc(D.dims[k][i]) + code + '</span></label>'; li.dataset.s = (D.dims[k][i] + " " + (k === "p" ? D.dims.pc[i] : "")).toLowerCase(); ul.appendChild(li);
                    });
                    ul.addEventListener("change", e => { const i = +e.target.value; e.target.checked ? S[k].add(i) : S[k].delete(i); T.page = 0; update(); });
                    pan.querySelector("input[type=search]").addEventListener("input", e => { const q = e.target.value.toLowerCase().trim(); ul.querySelectorAll("li").forEach(li => li.hidden = q && !li.dataset.s.includes(q)); });
                    pan.addEventListener("click", e => { const a = e.target.dataset && e.target.dataset.a; if (a === "none") { S[k].clear(); T.page = 0; update(); } if (a === "close") pan.hidden = true; });
                    btn.onclick = () => { const was = pan.hidden; ROOT.querySelectorAll(".ddp").forEach(p => p.hidden = true); pan.hidden = !was; if (was) pan.querySelector("input").focus(); };
                });
            }
            document.addEventListener("click", e => { if (!e.composedPath().some(n => n.classList && n.classList.contains("dd"))) ROOT.querySelectorAll(".ddp").forEach(p => p.hidden = true); });

            function renderChips() {
                const el = $("#chips"); el.innerHTML = "";
                const add = (lab, txt, fn) => { const s = document.createElement("span"); s.className = "chip"; s.innerHTML = "<em>" + lab + ":</em> " + esc(txt) + ' <button aria-label="Quitar filtro">×</button>'; s.querySelector("button").onclick = fn; el.appendChild(s); };
                [...S.y].sort().forEach(i => add("Año", YEARS[i], () => { S.y.delete(i); update(); }));
                [...S.m].sort((a, b) => a - b).forEach(m => add("Mes", MESL[m - 1], () => { S.m.delete(m); update(); }));
                [["v", "Vendedor"], ["p", "Producto"]].forEach(([k, l]) => [...S[k]].forEach(i => add(l, D.dims[k][i], () => { S[k].delete(i); update(); })));
            }

            function renderKPI() {
                const r = R, el = $("#kpis"); const nm = (k, i) => i < 0 ? "—" : esc(D.dims[k][i]);
                const share = (a, i) => i < 0 || !r.tot ? "" : pct(a[i] / r.tot) + " del total";
                let cmp = "";
                if (r.cmp && r.cmp.t > 0) {
                    const d = (r.tot - r.cmp.t) / r.cmp.t; const per = r.cmp.cut ? " al " + r.cmp.cut[1] + "/" + String(r.cmp.cut[0]).padStart(2, "0") : "";
                    cmp = '<div class="s">vs ' + r.cmp.year + per + ' (mismos filtros): <span class="delta ' + (d >= 0 ? "up" : "down") + '">' + (d >= 0 ? "▲ +" : "▼ ") + pct(d) + '</span> · ' + bs0(r.cmp.t) + '</div>';
                }
                el.innerHTML =
                    '<div class="panel kpi main"><div class="k">Ventas totales</div><div class="v num">' + bs(r.tot) + '</div><div class="s num">' + fmt(r.rows.length, 0) + ' líneas · ' + fmt(r.qty, 0) + ' unidades vendidas</div>' + cmp + '</div>' +
                    '<div class="panel kpi"><div class="k">Vendedores con ventas</div><div class="v num">' + r.nv + '</div><div class="s">de ' + D.dims.v.length + ' registrados</div></div>' +
                    '<div class="panel kpi"><div class="k">Productos vendidos</div><div class="v num">' + r.np + '</div><div class="s">de ' + D.dims.p.length + ' códigos</div></div>' +
                    '<div class="panel kpi"><div class="k">Producto más vendido</div><div class="name">' + nm("p", r.tp) + '</div><div class="s num">' + (r.tp < 0 ? "" : bs0(r.fP[r.tp]) + " · " + share(r.fP, r.tp)) + '</div></div>' +
                    '<div class="panel kpi"><div class="k">Vendedor con más ventas</div><div class="name">' + nm("v", r.tv) + '</div><div class="s num">' + (r.tv < 0 ? "" : bs0(r.fV[r.tv]) + " · " + share(r.fV, r.tv)) + '</div></div>';
            }

            const COL = { v: "var(--vend)", p: "var(--prod)" };
            function renderBars(k) {
                const el = $("#ch-" + k), agg = R["ch" + k.toUpperCase()], sel = S[k];
                let items = []; let sum = 0;
                for (let i = 0; i < agg.t.length; i++) { sum += agg.t[i]; if (agg.l[i] > 0 || sel.has(i)) items.push(i); }
                items.sort((a, b) => agg.t[b] - agg.t[a]);
                if (k === "p") {
                    const q = $("#psearch").value.toLowerCase().trim(); const tot = items.length;
                    if (q) items = items.filter(i => (D.dims.p[i] + " " + D.dims.pc[i]).toLowerCase().includes(q));
                    $("#p-count").textContent = (q ? items.length + " de " : "") + tot + " productos · clic para filtrar";
                }
                const max = items.reduce((m, i) => Math.max(m, agg.t[i]), 0) || 1;
                el.classList.toggle("dim", sel.size > 0);
                if (!items.length) { el.innerHTML = '<div class="empty">Sin ventas para la selección actual.</div>'; return; }
                const frag = document.createDocumentFragment();
                items.forEach(i => {
                    const d = document.createElement("div"); d.className = "row" + (sel.has(i) ? " on" : ""); d.tabIndex = 0; d.setAttribute("role", "button"); d.setAttribute("aria-pressed", sel.has(i));
                    const w = Math.max(0, agg.t[i] / max * 100);
                    d.innerHTML = '<div class="nm">' + esc(D.dims[k][i]) + (k === "p" ? "<code>" + esc(D.dims.pc[i]) + "</code>" : "") + '</div><div class="tr"><div class="fill" style="width:calc(' + w.toFixed(2) + '% - ' + (w / 100 * 120).toFixed(1) + 'px);background:' + COL[k] + '"></div><span class="lb num">' + fmt(Math.round(agg.t[i]), 0) + ' <i>' + (sum ? pct(agg.t[i] / sum) : "") + '</i></span></div>';
                    d.onclick = e => toggle(k, i, e); d.onkeydown = e => { if (e.key === "Enter" || e.key === " ") { e.preventDefault(); toggle(k, i, e); } };
                    tipOn(d, () => tipHTML(D.dims[k][i] + (k === "p" ? " (" + D.dims.pc[i] + ")" : ""), agg.t[i], agg.q[i], agg.l[i], sum ? agg.t[i] / sum : null));
                    frag.appendChild(d);
                });
                el.innerHTML = ""; el.appendChild(frag);
            }

            function renderMonthly() {
                const el = $("#ch-m"); const W = Math.max(320, el.clientWidth - 20), H = Math.round(Math.min(380, Math.max(260, W * 0.42)));
                const ys = YEARS.map((y, i) => i).filter(i => !S.y.size || S.y.has(i));
                const pal = ["var(--y1)", "var(--y2)", "var(--vend)", "var(--canal)"];
                const yc = i => { const off = YEARS.length - 1 - i; return off === 0 ? "var(--y2)" : off === 1 ? "var(--y1)" : pal[(off + 1) % pal.length]; };
                $("#m-legend").innerHTML = ys.map(i => '<span><i style="background:' + yc(i) + '"></i>' + YEARS[i] + '</span>').join("");
                let max = 0; ys.forEach(y => { for (let m = 1; m <= 12; m++)max = Math.max(max, R.chM.t[y * 13 + m]); });
                const step = max > 0 ? niceStep(max / 4) : 1; const top = Math.max(step, Math.ceil(max / step) * step);
                const L = 46, Rr = 8, Tp = 22, B = 26, pw = W - L - Rr, ph = H - Tp - B, gw = pw / 12, bw = Math.min(34, (gw - 8) / Math.max(1, ys.length));
                let s = '<svg viewBox="0 0 ' + W + ' ' + H + '" width="100%" role="img" aria-label="Ventas por mes y año">';
                for (let v = 0; v <= top + 1e-6; v += step) { const y = Tp + ph - v / top * ph; s += '<line class="gl" x1="' + L + '" x2="' + (W - Rr) + '" y1="' + y + '" y2="' + y + '"/><text class="ax" x="' + (L - 6) + '" y="' + (y + 4) + '" text-anchor="end">' + fmt(v / 1e6, step < 1e6 ? 1 : 0) + 'M</text>'; }
                const showLab = bw >= 13;
                for (let m = 1; m <= 12; m++) {
                    const gx = L + (m - 1) * gw; const on = !S.m.size || S.m.has(m);
                    s += '<rect class="mbar" data-m="' + m + '" x="' + gx + '" y="' + Tp + '" width="' + gw + '" height="' + ph + '" fill="transparent"/>';
                    ys.forEach((y, j) => {
                        const t = R.chM.t[y * 13 + m]; const h = t / top * ph; const x = gx + (gw - bw * ys.length) / 2 + j * bw;
                        s += '<rect class="mbar" data-m="' + m + '" data-y="' + y + '" x="' + (x + 1) + '" y="' + (Tp + ph - h) + '" width="' + (bw - 2) + '" height="' + Math.max(0, h) + '" fill="' + yc(y) + '" opacity="' + (on ? 1 : .3) + '" rx="1.5"/>';
                        if (showLab && t > 0) { const ty = Tp + ph - h - 4; s += '<text class="num" font-size="' + (bw >= 22 ? 10.5 : 9) + '" text-anchor="middle" x="' + (x + bw / 2) + '" y="' + ty + '" opacity="' + (on ? 1 : .4) + '" pointer-events="none">' + fmt(t / 1e6, 1) + '</text>'; }
                    });
                    s += '<text class="ax" x="' + (gx + gw / 2) + '" y="' + (H - 8) + '" text-anchor="middle"' + (S.m.has(m) ? ' style="font-weight:700;fill:var(--fg)"' : '') + '>' + MES[m - 1] + '</text>';
                }
                s += '</svg>'; el.innerHTML = s;
                el.querySelectorAll(".mbar").forEach(r => {
                    const m = +r.dataset.m; r.addEventListener("click", e => toggle("m", m, e));
                    tipOn(r, () => {
                        let h = "<b>" + MESL[m - 1][0].toUpperCase() + MESL[m - 1].slice(1) + "</b><div class='t2'>"; let prev = null;
                        ys.forEach(y => {
                            const t = R.chM.t[y * 13 + m]; h += "<span>" + YEARS[y] + "</span><span>" + bs(t) + "</span>";
                            if (prev !== null && prev > 0) { h += "<span>Var. vs " + YEARS[y - 1] + "</span><span>" + ((t - prev) >= 0 ? "+" : "") + pct((t - prev) / prev) + "</span>"; } prev = (ys.includes(y) ? t : null);
                        });
                        return h + "</div>";
                    });
                });
            }
            function niceStep(x) { const p = Math.pow(10, Math.floor(Math.log10(x))); const f = x / p; return (f <= 1 ? 1 : f <= 2 ? 2 : f <= 2.5 ? 2.5 : f <= 5 ? 5 : 10) * p; }

            const COLS = [
                { k: "f", l: "Fecha", v: i => D.cols.f[i], s: i => dstr(D.cols.f[i]) },
                { k: "y", l: "Año", v: i => YEARS[YR[i]], s: i => YEARS[YR[i]] },
                { k: "m", l: "Mes", v: i => MO[i], s: i => MES[MO[i] - 1] },
                { k: "n", l: "Nro. trans.", v: i => D.cols.n[i], s: i => D.cols.n[i] },
                { k: "v", l: "Vendedor", v: i => D.dims.v[D.cols.v[i]], s: i => D.dims.v[D.cols.v[i]], w: 1 },
                { k: "pc", l: "Código", v: i => D.dims.pc[D.cols.pc[i]], s: i => D.dims.pc[D.cols.pc[i]] },
                { k: "p", l: "Producto", v: i => D.dims.p[D.cols.p[i]], s: i => D.dims.p[D.cols.p[i]], w: 1 },
                { k: "q", l: "Cantidad", r: 1, v: i => D.cols.q[i], s: i => fmt(D.cols.q[i], 0) },
                { k: "u", l: "UM", v: i => D.dims.u[D.cols.u[i]], s: i => D.dims.u[D.cols.u[i]] },
                { k: "pr", l: "Precio", r: 1, v: i => D.cols.pr[i], s: i => fmt(D.cols.pr[i], 2) },
                { k: "t", l: "Total (Bs)", r: 1, v: i => D.cols.t[i], s: i => fmt(D.cols.t[i], 2) },
                { k: "cl", l: "Cliente", v: i => D.dims.cl[D.cols.cl[i]], s: i => D.dims.cl[D.cols.cl[i]], w: 1 }];

            function tableRows() {
                let rows = R.rows; const q = T.q.toLowerCase().trim();
                if (q) { const c = D.cols, dm = D.dims; rows = rows.filter(i => (dm.cl[c.cl[i]] + "|" + dm.v[c.v[i]] + "|" + dm.p[c.p[i]] + "|" + dm.pc[c.pc[i]] + "|" + c.n[i] + "|" + dstr(c.f[i])).toLowerCase().includes(q)); }
                const col = COLS.find(c => c.k === T.sort); const d = T.dir;
                rows = rows.slice().sort((a, b) => { const x = col.v(a), y = col.v(b); return (x < y ? -1 : x > y ? 1 : a - b) * d; });
                return rows;
            }
            function renderTable() {
                const rows = tableRows(); T.cache = rows;
                const pages = Math.max(1, Math.ceil(rows.length / T.size)); if (T.page >= pages) T.page = pages - 1;
                const th = $("#tbl thead"); th.innerHTML = "<tr>" + COLS.map(c => '<th data-k="' + c.k + '" class="' + (c.r ? "r" : "") + '" aria-sort="' + (T.sort === c.k ? (T.dir > 0 ? "ascending" : "descending") : "none") + '">' + c.l + (T.sort === c.k ? '<span class="ar">' + (T.dir > 0 ? "▲" : "▼") + '</span>' : "") + "</th>").join("") + "</tr>";
                th.querySelectorAll("th").forEach(h => h.onclick = () => { const k = h.dataset.k; if (T.sort === k) T.dir *= -1; else { T.sort = k; T.dir = (k === "t" || k === "q" || k === "pr" || k === "f") ? -1 : 1; } T.page = 0; renderTable(); });
                const st = T.page * T.size, sl = rows.slice(st, st + T.size);
                $("#tbl tbody").innerHTML = sl.length ? sl.map(i => "<tr>" + COLS.map(c => '<td class="' + (c.r ? "r num" : "") + (c.w ? " wrap" : "") + '">' + esc(c.s(i)) + "</td>").join("") + "</tr>").join("") : '<tr><td colspan="' + COLS.length + '" class="empty">Sin registros para los filtros aplicados.</td></tr>';
                let tq = 0, tt = 0; for (const i of rows) { tq += D.cols.q[i]; tt += D.cols.t[i]; }
                $("#tbl tfoot").innerHTML = '<tr><td colspan="7">Total de ' + fmt(rows.length, 0) + ' líneas filtradas</td><td class="r num">' + fmt(tq, 0) + '</td><td></td><td></td><td class="r num">' + fmt(tt, 2) + '</td><td></td></tr>';
                $("#t-info").textContent = T.q ? "Búsqueda aplicada sobre los filtros" : "Responde a todos los filtros";
                const pg = $("#pager"); pg.innerHTML = '<span>Mostrando ' + (rows.length ? fmt(st + 1, 0) : 0) + '–' + fmt(Math.min(st + T.size, rows.length), 0) + ' de ' + fmt(rows.length, 0) + '</span><span class="pg"><button class="btn" data-p="0">«</button><button class="btn" data-p="-1">‹</button><span class="num">Página ' + (T.page + 1) + ' de ' + pages + '</span><button class="btn" data-p="1">›</button><button class="btn" data-p="9e9">»</button></span>';
                pg.querySelectorAll("button").forEach(b => b.onclick = () => { const p = +b.dataset.p; T.page = p === 0 ? 0 : p === 9e9 ? pages - 1 : Math.min(pages - 1, Math.max(0, T.page + p)); renderTable(); });
            }
            let tq; $("#tsearch").addEventListener("input", e => { clearTimeout(tq); tq = setTimeout(() => { T.q = e.target.value; T.page = 0; renderTable(); }, 180); });
            $("#psearch").addEventListener("input", () => renderBars("p"));

            function exportRows() {
                const rows = T.cache || []; const head = COLS.map(c => c.l);
                const data = rows.map(i => COLS.map(c => c.k === "f" ? dstr(D.cols.f[i]) : c.k === "m" ? MO[i] : c.v(i))); return [head].concat(data);
            }
            function save(filename, data) { const blob = data instanceof Blob ? data : new Blob([data]); const a = document.createElement("a"); a.href = URL.createObjectURL(blob); a.download = filename; document.body.appendChild(a); a.click(); setTimeout(() => { URL.revokeObjectURL(a.href); a.remove(); }, 500); }
            const stamp = () => new Date().toISOString().slice(0, 10);
            $("#csv").onclick = () => {
                const rows = exportRows(); const csv = "\ufeff" + rows.map(r => r.map(v => { if (typeof v === "number") return String(v).replace(".", ","); v = String(v); return /[;"\n]/.test(v) ? '"' + v.replace(/"/g, '""') + '"' : v; }).join(";")).join("\r\n");
                save("ventas_filtradas_" + stamp() + ".csv", csv);
            };
            $("#xlsx").onclick = () => {
                if (!window.XLSX) { note("La exportación a Excel necesita conexión a internet para cargar la librería. Use Exportar CSV.", true); return; }
                const ws = XLSX.utils.aoa_to_sheet(exportRows()); ws["!cols"] = COLS.map(c => ({ wch: c.w ? 34 : 12 })); const wb = XLSX.utils.book_new(); XLSX.utils.book_append_sheet(wb, ws, "Ventas filtradas");
                const out = XLSX.write(wb, { bookType: "xlsx", type: "array" }); save("ventas_filtradas_" + stamp() + ".xlsx", new Blob([out], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" }));
            };

            const tip = $("#tip");
            function tipHTML(title, t, q, l, share) {
                let h = "<b>" + esc(title) + "</b><div class='t2'><span>Importe</span><span>" + bs(t) + "</span>";
                if (share != null) h += "<span>Participación</span><span>" + pct(share) + "</span>"; if (q != null) h += "<span>Cantidad</span><span>" + fmt(q, 0) + "</span>"; if (l != null) h += "<span>Líneas</span><span>" + fmt(l, 0) + "</span>"; return h + "</div>";
            }
            function tipOn(el, fn) {
                el.addEventListener("mousemove", e => {
                    tip.innerHTML = fn(); tip.hidden = false; const r = tip.getBoundingClientRect(); let x = e.clientX + 14, y = e.clientY + 14;
                    if (x + r.width > innerWidth - 8) x = e.clientX - r.width - 14; if (y + r.height > innerHeight - 8) y = e.clientY - r.height - 14; tip.style.left = Math.max(8, x) + "px"; tip.style.top = Math.max(8, y) + "px";
                });
                el.addEventListener("mouseleave", () => tip.hidden = true);
            }

            function renderQA() {
                const q = D.qa || {}, el = $("#qa-list"); let s = 0; for (const t of D.cols.t) s += t;
                const okT = Math.abs(s - D.meta.totalOrigen) < 0.01;
                const li = [];
                li.push('Total general del tablero <b>' + bs(s) + '</b> vs total reportado por el origen de datos <b>' + bs(D.meta.totalOrigen) + '</b>: ' + (okT ? '<span class="ok">coincide</span>' : '<span class="bad">no coincide</span>'));
                li.push('Líneas leídas: <b>' + fmt(N, 0) + '</b> (de ' + fmt(D.meta.filas, 0) + ' en el origen). Fechas del ' + dstr(D.meta.desde) + ' al ' + dstr(D.meta.hasta) + '.');
                el.innerHTML = li.map(x => "<li>" + x + "</li>").join("");
            }
            function note(t, err) { const m = $("#msg"); m.className = "notice" + (err ? " err" : ""); m.textContent = t; m.hidden = false; clearTimeout(note.t); note.t = setTimeout(() => m.hidden = true, err ? 12000 : 8000); }

            const clean = s => s == null ? "" : String(s).replace(/\s+/g, " ").trim();
            function toDay(v) {
                if (v instanceof Date && !isNaN(v)) return Math.round(Date.UTC(v.getFullYear(), v.getMonth(), v.getDate()) / 864e5);
                if (typeof v === "number") return Math.round(v - 25569);
                if (typeof v === "string") {
                    const s = v.trim(); let m = s.match(/^(\d{1,2})[\/\.-](\d{1,2})[\/\.-](\d{2,4})/); if (m) { let y = +m[3]; if (y < 100) y += 2000; return Math.round(Date.UTC(y, +m[2] - 1, +m[1]) / 864e5); }
                    m = s.match(/^(\d{4})-(\d{1,2})-(\d{1,2})/); if (m) return Math.round(Date.UTC(+m[1], +m[2] - 1, +m[3]) / 864e5);
                }
                return null;
            }
            const numv = v => { if (typeof v === "number") return v; if (v == null || v === "") return 0; let s = String(v).trim(); if (/,\d{1,4}$/.test(s)) s = s.replace(/\./g, "").replace(",", "."); else s = s.replace(/,/g, ""); const n = parseFloat(s); return isNaN(n) ? 0 : n; };

            function encode(recs, meta, qa) {
                recs.sort((a, b) => a.f - b.f);
                const dict = arr => { const a = [...new Set(arr)].sort((x, y) => x.localeCompare(y, "es")); const m = new Map(a.map((s, i) => [s, i])); return [a, m]; };
                const [V, mv] = dict(recs.map(r => r.v)), [P, mp] = dict(recs.map(r => r.p)), [PC, mpc] = dict(recs.map(r => r.code)), [U, mu] = dict(recs.map(r => r.u)), [CL, mcl] = dict(recs.map(r => r.cl));
                const cols = { f: [], n: [], v: [], p: [], pc: [], u: [], q: [], pr: [], t: [], cl: [] };
                recs.forEach(r => {
                    cols.f.push(r.f);
                    cols.n.push(r.n);
                    cols.v.push(mv.get(r.v));
                    cols.pc.push(mpc.get(r.code));
                    cols.p.push(mp.get(r.p));
                    cols.u.push(mu.get(r.u));
                    cols.q.push(r.q);
                    cols.pr.push(r.pr);
                    cols.t.push(r.t);
                    cols.cl.push(mcl.get(r.cl));
                });
                return { meta, dims: { v: V, pc: PC, p: P, u: U, cl: CL }, cols, qa };
            }

            $("#clear").onclick = () => { S.y.clear(); S.m.clear(); S.v.clear(); S.p.clear(); T.q = ""; $("#tsearch").value = ""; $("#psearch").value = ""; T.page = 0; update(); };
            $("#multi").onclick = () => { S.multi = !S.multi; $("#multi").setAttribute("aria-pressed", S.multi); };
            let rz; addEventListener("resize", () => { clearTimeout(rz); rz = setTimeout(() => R && renderMonthly(), 120); });

            function cargar(rows, meta) {
                meta = meta || {};
                const recs = []; let tot = 0, cero = 0, bad = 0; const dup = [0, 0], seen = new Set();
                (rows || []).forEach(r => {
                    const f = toDay(r.Fecha || r.fecha); if (f == null) { bad++; return; }
                    const t = numv(r.Total || r.total); tot += t;

                    const nroVal = r["Nro Trans"] != null ? r["Nro Trans"] : r.nro;
                    const vendVal = r.Vendedor != null ? r.Vendedor : r.vendedor;
                    const cliVal = r.Cliente != null ? r.Cliente : (r.cliente != null ? r.cliente : "—");
                    const codProdVal = r["Cod Producto"] != null ? r["Cod Producto"] : (r.codProducto != null ? r.codProducto : "");
                    const prodVal = r["Nombre Producto"] != null ? r["Nombre Producto"] : (r.producto != null ? r.producto : "SIN PRODUCTO");
                    const cantVal = r.Cantidad != null ? r.Cantidad : r.cantidad;
                    const umVal = r.UM != null ? r.UM : r.um;
                    const precVal = r.Precio != null ? r.Precio : r.precio;

                    const key = [f, nroVal, vendVal, cliVal, codProdVal, cantVal, precVal, t].join("|");
                    if (seen.has(key)) { dup[0]++; dup[1] += t; } else seen.add(key);
                    if (t === 0) cero++;

                    recs.push({
                        f: f,
                        n: nroVal == null ? "" : String(nroVal),
                        v: clean(vendVal) || "SIN VENDEDOR",
                        code: clean(codProdVal) || "SIN COD",
                        p: clean(prodVal) || "SIN PRODUCTO",
                        q: numv(cantVal),
                        u: clean(umVal) || "—",
                        pr: numv(precVal),
                        t: t,
                        cl: clean(cliVal) || "—"
                    });
                });
                if (!recs.length) { note("No hay ventas para mostrar.", true); return; }
                const qa = { dup, cero };
                if (bad) qa.faltan = [bad + " filas sin fecha válida (excluidas)"];
                setData(encode(recs, { fuente: meta.fuente || "sistema", filas: rows.length, totalOrigen: meta.totalOrigen != null ? +meta.totalOrigen : tot }, qa));
            }

            window.DashVentas = { cargar };
            if (Array.isArray(window.DV_DATOS)) cargar(window.DV_DATOS, window.DV_META);
        })();
    </script>
</asp:Content>