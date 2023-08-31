let datatable;

$(document).ready(function () {
    loadAllData();
});

function loadAllData() {
    datatable = $("#tblData").DataTable({
        "language": dataTableLanguage,
        "ajax": {
            "url":"/Admin/Producto/GetAll"
        },
        "columns": [
            {"data": "numeroSerie"},
            {"data": "nombre" },
            { "data": "categoria.nombre"},
            { "data": "marca.nombre" },
            {
                "data": "precio", "className": "text-end",
                "render": function (data) {
                    var d = data.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
                    return d;   
                }
            },
            {
                "data": "estado",
                "render": function (data) {
                    if (data==true)
                        return `<span class="badge text-bg-success rounded-pill">Activo</span>`;
                    return `<span class="badge text-bg-danger rounded-pill">Inactivo</span>`;
                }, "width": "10%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div>
                            <a href="/Admin/Producto/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                <i class="bi bi-pencil-square"></i>
                            </a>
                            <a onclick=Delete("/Admin/Producto/Delete/${data}") 
                                class="btn btn-danger text-white" style="cursor:pointer">
                                <i class="bi bi-trash3-fill"></i>
                            </a>
                        </div>
                    `;
                }, "width": "20%"
            }
        ]
    });
}

