let datatable;

$(document).ready(function () {
    loadAllData();
});

function loadAllData() {
    datatable = $("#tblData").DataTable({
        "language": {
            "lengthMenu": "Mostrar _MENU_ Registros Por Pagina",
            "zeroRecords": "Aun no se han agregado registros",
            "info": "Mostrar pagina _PAGE_ de _PAGES_",
            "infoEmpty": "no hay registros",
            "infoFiltered": "(filtered from _MAX_ total registros)",
            "search": "Buscar",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "ajax": {
            "url":"/Admin/Almacen/GetAll"
        },
        "columns": [
            {"data":"nombre","width":"20%"},
            { "data": "descripcion", "width":"50%"},
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
                            <a href="/Admin/Almacen/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                <i class="bi bi-pencil-square"></i>
                            </a>
                            <a onclick=Delete("/Admin/Almacen/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                <i class="bi bi-trash3-fill"></i>
                            </a>
                        </div>
                    `;
                }, "width": "20%"
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Realmente eliminara el Almacen?",
        text: "Recuerde: una vez eliminado no podra recuperarlo",
        icon: "warning",
        buttons: ["No", "Si"],
        dangerMode: true
    }).then((borrar) => {
        if (borrar) {
            $.ajax({
                type: "POST",
                url: url,
                success: function myfunction(data) {
                    if (data.success) {
                        toastr.success(data.message);
                        datatable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}