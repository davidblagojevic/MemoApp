var editButton;
var detailsButton;

var addModalScrollable;
var doAddButton;

var idInput;
var titleInput;
var noteTextArea;
var tagInput;

var doAddMemoButton;

//var doEditButton;


var tableGrid;

$(document).ready(function () {

    //nece da radi sa $
    addButton = $('#add');
    detailsButton = $('.details');

    tableGrid = $('#tableGrid');

    //setting up datatable
    tableGrid.dataTable({
        "columnDefs": [{
            "targets": [4, 5],
            "ordering": false,
            "orderable": false
        }]
        //"processing": true,
        //"serverSide": true,
        //"filter": true,
        //"orderMulti": false,
        //"pageLength": 5,

    });

    //klikovi

    $(document).on('click', '.addModalClose', function (event) {
        addModalScrollable.modal('hide');
    });

    //klikovi za input
    $(document).on('click', '#doAddMemo', function (e) {
        params = {
            "Id": $(idInput).val(),
            "Title": $(titleInput).val(),
            "Note": $(noteTextArea).val(),
            "TagList": $(tagInput).val().trimEnd().trimStart().split(" ")
        };


        $.post("/Memo/AddOrEditMemo", params, function (data) {
            if (data.outcome == "0") {
                location.reload();
            }
            if (data.outcome == "1") {
                bootbox.alert('Something went wrong, please try later');
            }
        });

        $(detailsButton).click(function (e) {

            e.preventDefault();
        });


        e.preventDefault();
    });





});

//new for modals with bootbox
function deleteMemo(id, memoName) {
    bootbox.confirm({
        title: "Are you sure?",
        message: `Do you want to delete the Memo (${memoName}) now? This cannot be undone.`,
        buttons: {
            cancel: {
                label: '<i class="fa fa-times"></i> Cancel'
            },
            confirm: {
                label: '<i class="fa fa-check"></i> Confirm'
            }
        },
        callback: function (result) {
            console.log('This was logged in the callback: ' + result);
            if (result) {
            $.post("/Memo/Delete/" + id, function (data) {
                if (data.outcome == "0") {
                    location.reload();
                }
                if (data.outcome == "1") {
                    bootbox.alert('Something went wrong!');
                }
            });

            }
        }
    });
}

function editMemo(id) {
    $.ajax({
        url: '/Memo/GetAddEditModal/' + id,
        datatype: "html",
        type: "get",
        contenttype: 'application/json; charset=utf-8',
        success: function (data) {
            $("#memoModal").html(data);


            addModalScrollable = $('#addModalScrollable');
            $(addModalScrollable).modal("show");

            //za input
            idInput = $('#idInput');
            titleInput = $('#titleInput');
            noteTextArea = $('#noteTextArea');
            tagInput = $('#tagInput');
            doAddMemoButton = $('#doAddMemo');
        },
        error: function (xhr) {
            bootbox.alert('error');
        } 
    })
}

function addMemo() {
    $.ajax({
        url: '/Memo/GetAddEditModal/',
        datatype: "json",
        type: "get",
        contenttype: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            $("#memoModal").html(data);


            addModalScrollable = $('#addModalScrollable');
            $(addModalScrollable).modal("show");

            //za input
            idInput = $('#idInput');
            titleInput = $('#titleInput');
            noteTextArea = $('#noteTextArea');
            tagInput = $('#tagInput');
            doAddMemoButton = $('#doAddMemo');
        },
        error: function (xhr) {
            bootbox.alert('error');
        }
    })
}