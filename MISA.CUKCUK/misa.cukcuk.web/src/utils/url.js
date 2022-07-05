const baseUrl = 'http://localhost:52039/api/v1'
const ApiUrl = Object.freeze({
    Material: Object.freeze({
        Insert: `${baseUrl}/Materials/`,
        Update: `${baseUrl}/Materials/`,
        Delete: `${baseUrl}/Materials/`,
        GetAll: `${baseUrl}/Materials/`,
        GetById: `${baseUrl}/Materials/`,
        GetPaging: `${baseUrl}/Materials/filter`,
        NewMaterialCode: `${baseUrl}/Materials/NewMaterialCode/`
    }),
    UnitConversion: Object.freeze({
        Insert: `${baseUrl}/UnitConversions/`,
        Update: `${baseUrl}/UnitConversions/`,
        Delete: `${baseUrl}/UnitConversions/`,
        GetAll: `${baseUrl}/UnitConversions/`,
        GetById: `${baseUrl}/UnitConversions/`,
        GetByMaterialId: `${baseUrl}/UnitConversions/Material/`,
        GetPaging: `${baseUrl}/UnitConversions/filter`,
    }),
    MasterData: Object.freeze({
        GetStock: `${baseUrl}/MasterDatas/Stock/`,
        GetUnit: `${baseUrl}/MasterDatas/Unit/`
    })
})

export default ApiUrl;