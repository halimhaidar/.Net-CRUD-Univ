$(document).ready(function () {
    $('#tbUniversity').DataTable({
        "ajax": {
            url: "https://localhost:44395/api/University",
            type: "GET",
            "dataType": "json",
            "dataSrc": "data",
            //success: function (result) {
            //    console.log(result)
            //}
        },
        "columns": [
            {
                "render": function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { "data": "name" },
            {
                "render": function (data, type, row) {
                    return '<button class="btn btn-warning " data-placement="left" data-toggle="tooltip" data-animation="false" title="Edit" onclick="GetById(' + row.universityId + ')"><i class="fa fa-pen"></i></button >' + '&nbsp;' +
                        '<button class="btn btn-danger" data-placement="right" data-toggle="tooltip" data-animation="false" title="Delete" onclick="ConfirmDelete(' + row.universityId + ')"><i class="fa fa-trash"></i></button >'
                }
            }
        ]
    })
})


$("#addbutton").click(() => {
    $("#buttonSubmit").attr("onclick", "Save()");
    $("#buttonSubmit").attr("class", "btn btn-success");
    $("#buttonSubmit").html("Save");
    $("#universityId").val("");
    $("#name").val("");
})
function Save() {
    debugger;
    var University = new Object();
    University.name = $('#name').val();
    $.ajax({
        type: "POST",
        url: "https://localhost:44395/api/University",
        data: JSON.stringify(University),
        contentType: "application/json; charset=utf-8",
    }).then((result) => {
        if (result.status == 200) {
            $('#tbUniversity').DataTable().ajax.reload();
            swal("Data Berhasil Dimasukkan!", "You clicked the button!", "success");
        }
        else {
            swal("Data Gagal Dimasukkan!", "You clicked the button!", "error");
        }
    })
}

function GetById(id) {
    debugger;
    $.ajax({
        type: "GET",
        url: "https://localhost:44395/api/University/" + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            var obj = result.data;
            $('#universityId').val(obj.universityId);
            $('#name').val(obj.name);
            $("#buttonSubmit").attr("onclick", "Update()");
            $("#buttonSubmit").attr("class", "btn btn-warning");
            $("#buttonSubmit").html("Update");          
            $('#insertModal').modal('show');
            $('#tbUniversity').DataTable().ajax.reload();
        },
        error: function (errormesage) {
            swal("Data Gagal Dimasukkan!", "You clicked the button!", "error");
        }
    })
}

function Update() {
    debugger;
    var University = new Object();
    University.universityId = $('#universityId').val();
    University.name = $('#name').val();
    $.ajax({
        type: 'PUT',
        url: "https://localhost:44395/api/University/",
        data: JSON.stringify(University),
        contentType: "application/json; charset=utf-8",
    }).then((result) => {
        debugger;
        if (result.status == 200) {
            $('#tbUniversity').DataTable().ajax.reload();
            swal("Data Berhasil Diperbarui!", "You clicked the button!", "success");
        }
        else {
            swal("Data Gagal Diperbarui!", "You clicked the button!", "error");
        }
    });
}

/*function Delete(id) {
    //debugger;
    $.ajax({
        url: "https://localhost:44395/api/University/" + id,
        type: "DELETE",
        dataType: "json",
    }).then((result) => {
        debugger;
        if (result.status == 200) {
            $('#tbUniversity').DataTable().ajax.reload();
            swal("Data Berhasil Dihapus!", "You clicked the button!", "success");
        }
        else {
            swal("Data Gagal Dihapus!", "You clicked the button!", "error");
        }
    });
}*/
function Delete(id) {
    debugger;
    $.ajax({
        url: "https://localhost:44395/api/University/" + id,
        type: "DELETE",
        dataType: "json",
    }).then((result) => {
        if (result.status == 200) {
            $('#insertModal').modal('hide');
            $('#tbUniversity').DataTable().ajax.reload();

            swal({
                icon: 'success',
                title: 'Deleted',
                text: 'University deleted succesfully'
            });
        }
        else {
            swal({
                icon: 'error',
                title: 'Failed',
                text: 'Failed to delete Universty'
            });
        }
    });
}

function ConfirmDelete(id) {
    debugger;
    swal({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        buttons: true,
        dangerMode: true,
    }).then((isConfirmed) => {
        if (isConfirmed) {
            Delete(id);
            swal(
                'Deleted!',
                'Your file has been deleted.',
                'success'
            )
        }
    })
}