/****************************************
 *       Basic Table                   *
 ****************************************/
$('#zero_config').DataTable();

//=============================================//
//    File export                              //
//=============================================//
$('#file_export').DataTable({
    dom: 'Bfrtip',
    buttons: [
        'copy', 'csv', 'excel', 'pdf', 'print'
    ]
});
$('.buttons-copy, .buttons-csv, .buttons-print, .buttons-pdf, .buttons-excel').addClass('btn btn-primary mr-1');

//=============================================//
//     Row grouping                            //
//=============================================//
var table = $('#row_group').DataTable({
    "pageLength": 10,
    "columnDefs": [{
        "visible": false,
        "targets": 2
    }],
    "order": [
        [2, 'asc']
    ],
    "displayLength": 25,
    "drawCallback": function (settings) {
        var api = this.api();
        var rows = api.rows({
            page: 'current'
        }).nodes();
        var last = null;

        api.column(2, {
            page: 'current'
        }).data().each(function (group, i) {
            if (last !== group) {
                $(rows).eq(i).before(
                    '<tr class="group"><td colspan="5">' + group + '</td></tr>'
                );

                last = group;
            }
        });
    }
});