
const _employeeModel = {
    id: 0,
    name: "",
    departmentRef: 0,
    salary: 0,
    contractDate: ""
}

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
                                $("<button>").addClass("btn btn-primary btn-sm btn-edit-employee").text("Edit").data("dataEmployee", employee),
                                $("<button>").addClass("btn btn-danger btn-sm ms-2 btn-delete-employee").text("Delete").data("dataEmployee", employee),
                            )
                        )
                    )
                })
            }
        })
}

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

function ModalShow() {

    $("#txtName").val(_employeeModel.name);
    $("#selectDepartment").val(_employeeModel.departmentRef == 0 ? $("#selectDepartment option:first").val() : _employeeModel.departmentRef)
    $("#numSalary").val(_employeeModel.salary);
    $("#txtContractDate").val(_employeeModel.contractDate)


    $("#employeeModal").modal("show");

}