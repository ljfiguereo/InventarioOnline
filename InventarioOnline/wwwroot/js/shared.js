
let dataTableLanguage = {
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
};
function addAspValidation(fieldName, message) {
    var parent = document.querySelector('[data-valmsg-for="' + fieldName + '"]');

    var a = document.createElement("span");

    var temp = `<span id="${fieldName.toLowerCase()}-error">${message}</span>`;
    var a = document.createElement("span");
    a.innerHTML = temp;
    parent.appendChild(a.childNodes[0]);

    nombre.focus();
}

$("#noEnter").bind("keypress", function (e) {
    if (e.keyCode == 13) {
        return false;
    }
});

function Delete(url) {
    swal({
        title: "Realmente desea Eliminarla?",
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
