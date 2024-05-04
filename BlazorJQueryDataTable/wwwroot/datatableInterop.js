window.initializeDataTable = () => {
    $('#hidden-table-info').DataTable({
        searching: true
    });
};

function AddDataTable(table) {
    $(table).DataTable({
        searching: true
    });
}