$(document).ready(function () {
    $('#tbEducation').DataTable({
        "ajax": {
            url: "https://localhost:44395/api/Education",
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
            { "data": "degree" },
            { "data": "gpa" },
            { "data": "university.name" },
            {
                "render": function (data, type, row) {
                    return '<button class="btn btn-warning " data-placement="left" data-toggle="tooltip" data-animation="false" title="Edit" onclick="SelectUniversity();  GetById(' + row.id + ');"><i class="fa fa-pen"></i></button >' + '&nbsp;' +
                        '<button class="btn btn-danger" data-placement="right" data-toggle="tooltip" data-animation="false" title="Delete" onclick="ConfirmDelete(' + row.id + ')"><i class="fa fa-trash"></i></button >'
                }
            }
        ]
    })
})

function SelectUniversity() {
    debugger;
    $.ajax({
        url: 'https://localhost:44395/api/University',
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (result) {
            debugger;
            var obj = result.data;
            $('#university').empty();
            console.log(obj);
            for (var i = 0; i < obj.length; i++) {
                $('#university').append('<option value="' + obj[i].universityId + '">' + obj[i].name + '</option>');
            }

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

$("#addbutton").click(() => {
    $("#buttonSubmit").attr("onclick", "Save()");
    $("#buttonSubmit").attr("class", "btn btn-success");
    $("#buttonSubmit").html("Add");
    $("#degree").val("");
    $("#gpa").val("");
    $("#university").val("");
})

function Save() {
    debugger;
    var Education = new Object();
    Education.degree = $('#degree').val();
    Education.gpa = $('#gpa').val();
    Education.university_Id = $('#university').val();
    $.ajax({
        type: "POST",
        url: "https://localhost:44395/api/education",
        data: JSON.stringify(Education),
        contentType: "application/json; charset=utf-8",
    }).then((result) => {
        if (result.status == 200) {
            $('#tbEducation').DataTable().ajax.reload();
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
        url: "https://localhost:44395/api/education/" + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            var obj = result.data;
            $('#educationId').val(obj.id);
            $('#degree').val(obj.degree);
            $('#gpa').val(obj.gpa);
            $('#university').val(obj.university_Id);
            $("#buttonSubmit").attr("onclick", "Update()");
            $("#buttonSubmit").attr("class", "btn btn-warning");
            $("#buttonSubmit").html("Update");
            $('#insertModal').modal('show');
            $('#tbUniversity').DataTable().ajax.reload();
        },
        error: function (errormesage) {
            swal("Data Gagal DiRubah!", "You clicked the button!", "error");
        }
    })
}

function Update() {
    debugger;
    var Education = new Object();
    Education.Id = $('#educationId').val();
    Education.degree = $('#degree').val();
    Education.gpa = $('#gpa').val();
    Education.university_Id = $('#university').val();
    $.ajax({
        type: 'PUT',
        url: "https://localhost:44395/api/education/",
        data: JSON.stringify(Education),
        contentType: "application/json; charset=utf-8",
    }).then((result) => {
        debugger;
        if (result.status == 200) {
            $('#tbEducation').DataTable().ajax.reload();
            swal("Data Berhasil Diperbarui!", "You clicked the button!", "success");
        }
        else {
            swal("Data Gagal Diperbarui!", "You clicked the button!", "error");
        }
    });
}


function Delete(id) {
    debugger;
    $.ajax({
        url: "https://localhost:44395/api/education/" + id,
        type: "DELETE",
        dataType: "json",
    }).then((result) => {
        if (result.status == 200) {
            $('#insertModal').modal('hide');
            $('#tbEducation').DataTable().ajax.reload();
           
            swal({
                icon: 'success',
                title: 'Deleted',
                text: 'Education deleted succesfully'
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

/* Export Excel*/
function fnExcelReport() {
    debugger;
    var tab_text = "<table border='2px'><tr bgcolor='#87AFC6'>";
    var textRange; var j = 0;
    tab = document.getElementById('headerTable'); // id of table

    for (j = 0; j < tab.rows.length; j++) {
        tab_text = tab_text + tab.rows[j].innerHTML + "</tr>";
        //tab_text=tab_text+"</tr>";
    }

    tab_text = tab_text + "</table>";
    tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");//remove if u want links in your table
    tab_text = tab_text.replace(/<img[^>]*>/gi, ""); // remove if u want images in your table
    tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params

    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ");

    if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
    {
        txtArea1.document.open("txt/html", "replace");
        txtArea1.document.write(tab_text);
        txtArea1.document.close();
        txtArea1.focus();
        sa = txtArea1.document.execCommand("SaveAs", true, "Say Thanks to Sumit.xls");
    }
    else                 //other browser not tested on IE 11
        sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));

    return (sa);
}