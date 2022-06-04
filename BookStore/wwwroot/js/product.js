var dataTable;

$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "title", "width": "15%" },
            { "data": "description", "width": "15%" },
            { "data": "isbn", "width": "15%" },
            { "data": "author", "width": "15%" },
            { "data": "price", "width": "15%" },
            { "data": "listPrice", "width": "15%" },
            { "data": "category.categoryname", "width": "15%" },
            {
                "data": "productId", "render": function (data) {
                    return `
                        <div class="rounded-pill btn-group" >
                        <a href="/Admin/Product/Upsert?id=${data}"  class="btn alert-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>
                        <a asp-controller="Product" asp-action="Delete" asp-route-id="@obj.Id" class="btn alert-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
                    </div>
                    `
                }, "width": "15%" },
          




        ]
    });
}