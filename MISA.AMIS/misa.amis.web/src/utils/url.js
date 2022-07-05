const baseUrl = 'http://localhost:52039/api/v1';
const ApiUrl = Object.freeze({
    Employee: Object.freeze({
        Insert: `${baseUrl}/Employees/`,
        Update: `${baseUrl}/Employees/`,
        Delete: `${baseUrl}/Employees/`,
        Export: `${baseUrl}/Employees/exporter`,
        GetEmployees: `${baseUrl}/Employees/`,
        GetEmployeeById: `${baseUrl}/Employees/`,
        GetPaging: `${baseUrl}/Employees/filter`,
        NewEmployeeCode: `${baseUrl}/Employees/newEmployeeCode/`
    }),
    Department: Object.freeze({
        GetDepartments: `${baseUrl}/Departments`
    })

})

export default ApiUrl;