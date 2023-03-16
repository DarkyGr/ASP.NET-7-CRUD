// Employee Modal
const _employeeModel = {
    id: 0,
    name: "",
    departmentRef: 0,
    salary: 0,
    contractDate: ""
}

// Function to Employess Table
function EmployeesShow() {
    fetch("/Home/GetListEmployees")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then(responseJson => {
            if (responseJson.length > 0) {

                $("#employeesTable tbody").html("");


                responseJson.forEach((employee) => {
                    $("#employeesTable tbody").append(
                        $("<tr>").append(
                            $("<td>").text(employee.name),
                            $("<td>").text(employee.departmentRef.name),
                            $("<td>").text(employee.salary),
                            $("<td>").text(employee.contractDate),
                            $("<td>").append(
                                $("<button>").addClass("btn btn-primary btn-sm btn-edit-employee").text("Edit").data("employeeData", employee),
                                $("<button>").addClass("btn btn-danger btn-sm ms-2 btn-delete-employee").text("Delete").data("employeeData", employee),
                            )
                        )
                    )
                })
            }
        })
}

// Function to get department name for employee table
document.addEventListener("DOMContentLoaded", function () {

    EmployeesShow();

    fetch("/Home/GetListDepartments")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then(responseJson => {

            if (responseJson.length > 0) {
                responseJson.forEach((department) => {

                    $("#selectDepartment").append(
                        $("<option>").val(department.id).text(department.name)
                    )

                })
            }

        })

    $("#txtContractDate").datepicker({
        format: "dd/mm/yyyy",
        autoclose: true,
        todayHighlight: true
    })


}, false)

// Function to Show modal
function ModalShow() {

    $("#txtName").val(_employeeModel.name);
    $("#selectDepartment").val(_employeeModel.departmentRef == 0 ? $("#selectDepartment option:first").val() : _employeeModel.departmentRef);
    $("#numSalary").val(_employeeModel.salary);
    $("#txtContractDate").val(_employeeModel.contractDate);

    $("#employeeModal").modal("show");
}

// Show new employee modal on button click
$(document).on("click", ".btn-new-employee", function () {
    _employeeModel.id = 0;
    _employeeModel.name = "";
    _employeeModel.departmentRef = 0;
    _employeeModel.salary = 0;
    _employeeModel.contractDate = "";

    ModalShow();
})

// Show edit employee modal on button click
$(document).on("click", ".btn-edit-employee", function () {
    const _employee = $(this).data("employeeData");

    _employeeModel.id = _employee.id;
    _employeeModel.name = _employee.name;
    _employeeModel.departmentRef = _employee.departmentRef.id;
    _employeeModel.salary = _employee.salary;
    _employeeModel.contractDate = _employee.contractDate;

    ModalShow();
})


// Action for Save and Edit employee
$(document).on("click", ".btn-save-changes", function () {

    const model = {
        id: _employeeModel.id,
        name: $("#txtName").val(),
        departmentRef: {
            id: $("#selectDepartment").val()
        },
        salary: $("#numSalary").val(),
        contractDate: $("#txtContractDate").val()
    }

    // To Save
    if (_employeeModel.id == 0) {

        fetch("/Home/SaveEmployee", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(model)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => {                
                if (responseJson.value) {
                    $("#employeeModal").modal("hide");
                    Swal.fire("Done!", "The employee was created successfully", "success");
                    EmployeesShow();
                } else {
                    Swal.fire("Error!", "The employee could not be created", "error");
                }
            })
    } else {    // To Edit

        console.log(_employeeModel.id);
        fetch("/Home/EditEmployee", {
            method: "PUT",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(model)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {                
                if (responseJson.value) {
                    $("#employeeModal").modal("hide");
                    Swal.fire("Done", "The employee was edited successfully", "success");
                    EmployeesShow();
                } else {
                    Swal.fire("Error", "The user employee not be edited", "error");
                }
            })
    }
})