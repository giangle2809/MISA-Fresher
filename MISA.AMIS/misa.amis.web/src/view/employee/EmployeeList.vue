<template>
  <div class="ms-content-header">
    <div class="ms-content-title ms-title">Nhân viên</div>
    <div class="ms-space"></div>
    <ms-button
      :classList="'ms-btn-success'"
      @click="addEmployeeOnClick"
      :text="'Thêm mới nhân viên'"
    >
    </ms-button>
  </div>
  <div class="ms-content-body">
    <ms-table
      :data="employees"
      :totalPage="totalPage"
      :totalRecord="totalRecord"
      :tableConfig="tableConfig"
      :idCode="idCode"
      @refresh="refreshData"
      @search="filterData"
      @delete="showMessageDelete"
      @edit="editEmployee"
      @export="exportFile"
      @multiDelete="showMessageMultiDelete"
      @replication="replicationEmployee"
    />

    <employee-detail
      :employeeSelected="employeeIdSelected"
      :isShowDialog="isShowDialog"
      :eStatus="eStatus"
      @refresh="refreshData"
      @close="isShowDialog = false"
    />
  </div>

  <ms-loading :isShow="isShowLoading" />
  <ms-toast
    :mess="toastContent"
    :isShow="isShowToast"
    :status="statusToast"
    @close="closeToast"
  />
  <ms-popup-message
    :status="statusMess"
    :isShow="isShowMessage"
    :message="messageContent"
  >
    <ms-button
      :text="'Không'"
      @click="closePopUpMessage"
      v-if="eStatus == dataStatus.Delete || eStatus == dataStatus.MultiDelete"
    ></ms-button>
    <div class="ms-space"></div>
    <ms-button
      :text="'Có'"
      :classList="'ms-btn-success'"
      @click="responseMessage"
      v-if="eStatus == dataStatus.Delete || eStatus == dataStatus.MultiDelete"
    ></ms-button>
  </ms-popup-message>
</template>

<script>
import Constant from "@/utils/constant";
import EmployeeDetail from "@/view/employee/EmployeeDetail.vue";
import ApiUrl from "@/utils/url";
import Format from "@/utils/format";
import EmployeeRepository from "@/repository/employeeRepository";
import ResourceVN from "@/utils/resourceVN";

export default {
  name: "EmployeeList",
  components: { EmployeeDetail },
  data() {
    return {
      dataStatus: Constant.DataStatus,
      employees: [],
      isShowDialog: false,
      filter: {
        pageSize: "20",
        pageNumber: "1",
        filter: "",
      },
      employeeIdSelected: null,
      employeeIds: [],
      eStatus: Constant.DataStatus.Add,
      totalRecord: 0,
      totalPage: 0,
      isShowLoading: false,
      isShowMessage: false,
      messageContent: "",
      statusMess: "",
      isShowToast: false,
      toastContent: "",
      statusToast: "",
      tableConfig: [
        {
          columnName: "MÃ NHÂN VIÊN",
          columnCode: "EmployeeCode",
        },
        {
          columnName: "TÊN NHÂN VIÊN",
          columnCode: "FullName",
        },
        {
          columnName: "GIỚI TÍNH",
          columnCode: "GenderName",
        },
        {
          columnName: "NGÀY SINH",
          columnCode: "DateOfBirth",
        },
        {
          columnName: "SỐ CMND",
          columnCode: "IdentityNumber",
        },
        {
          columnName: "CHỨC DANH",
          columnCode: "PositionName",
        },
        {
          columnName: "TÊN ĐƠN VỊ",
          columnCode: "DepartmentName",
        },
        {
          columnName: "SỐ TÀI KHOẢN",
          columnCode: "BankAccountNumber",
        },
        {
          columnName: "TÊN NGÂN HÀNG",
          columnCode: "BankName",
        },
        {
          columnName: "CHI NHÁNH TÀI KHOẢN NGÂN HÀNG",
          columnCode: "BankBranchName",
        },
      ],
      idCode: "EmployeeId",
    };
  },
  async created() {
    await this.loadData();
  },
  methods: {
    /**
     * ShowToast
     * @param toastContent: Nội dung cần hiển thị
     * @param statusToast: Trạng thái hiển thị
     * @author LNTGiang
     */
    showToast(toastContent, statusToast) {
      this.toastContent = toastContent;
      this.statusToast = statusToast;
      this.isShowToast = true;
      setTimeout(() => {
        this.isShowToast = false;
      }, Constant.ToastTime);
    },

    /**
     * Ẩn Toast
     * @author LNTGiang
     */
    closeToast() {
      this.isShowToast = false;
    },

    /**
     * Hiển thị MessageBox
     * @param statusMess: Trạng thái của MessageBox
     * @param messageContent: Nội dung của MessageBox
     * @author LNTGiang
     */
    showPopUpMessage(statusMess, messageContent) {
      this.isShowMessage = true;
      this.statusMess = statusMess;
      this.messageContent = messageContent;
    },

    /**
     * Đóng MessageBox
     * @author LNTGiang
     */
    closePopUpMessage() {
      this.isShowMessage = false;
    },

    /**
     * show message box delete
     * @param employeeId: id của nhân viên cần xóa
     * @param employeeCpde: Mã nhân viên cần xóa
     * @author LNTGiang
     */
    showMessageDelete(employeeId, employeeCode) {
      this.eStatus = Constant.DataStatus.Delete;
      this.employeeIdSelected = employeeId;
      this.showPopUpMessage(
        Constant.MessageBoxStatus.Warn,
        Format.stringFormat(ResourceVN.EmployeeDelete, [employeeCode])
      );
    },

    /**
     * show message box multidelete
     * @param emplyeeIds: danh sách id nhân viên được chọn
     * @author LNTGiang
     */
    showMessageMultiDelete(employeeIds) {
      this.eStatus = Constant.DataStatus.MultiDelete;
      this.employeeIds = employeeIds;
      this.showPopUpMessage(
        Constant.MessageBoxStatus.Warn,
        ResourceVN.EmployeeMultiDelete
      );
    },

    /**
     * Khi click có trong messagebox
     * @author LNTGiang
     */
    responseMessage() {
      if (this.eStatus === Constant.DataStatus.Delete) {
        this.deleteEmployee();
      } else if (this.eStatus === Constant.DataStatus.MultiDelete) {
        this.multiDeleteEmployee();
      }
      this.closePopUpMessage();
    },

    /**
     * Click Thêm mới nhân viên
     * @author LNTGiang
     */
    addEmployeeOnClick() {
      try {
        // Gán trạng thái data là thêm
        this.eStatus = Constant.DataStatus.Add;

        // Mở Dialog nhân viên
        this.isShowDialog = true;

        // Gán mã nhân viên bằng null
        this.employeeIdSelected = null;
      } catch (ex) {
        console.log(ex);
      }
    },

    /**
     * Load dữ liệu nhân viên
     * @author LNTGiang
     */
    async loadData() {
      // Show loading
      this.isShowLoading = true;

      // Load dữ liệu
      try {
        if (this.filter) {
          let response = await EmployeeRepository.filter(
            this.filter["pageSize"],
            this.filter["pageNumber"],
            this.filter["filter"]
          );
          if (response) {
            if (response.data) {
              this.employees = response.data.Data;
            }
            // Format lại định dạng ngày
            this.employees.forEach((element) => {
              element.DateOfBirth = Format.fomatDate(
                element.DateOfBirth,
                "DD/MM/YYYY"
              );
            });

            this.totalRecord = response.data.TotalRecord;
            this.totalPage = response.data.TotalPage;
          }
        }
      } catch (error) {
        console.log(error);
      }

      // Đóng Loading
      this.isShowLoading = false;
    },
    /**
     * Refresh dữ liệu
     * @author LNTGiang
     */
    async refreshData() {
      try {
        // Gán pageNumber về 1 và xóa filter
        this.filter.pageNumber = 1;
        this.filter.employeeFilter = "";

        // Load dữ liệu
        await this.loadData();
      } catch (ex) {
        console.log(ex);
      }
    },
    /**
     * Thực hiện bộ lọc dữ liệu khi phân trang và tìm kiếm
     * @param filter: Bộ lọc
     * @author LNTGiang
     */
    async filterData(filter) {
      try {
        // Gán lại pageSize, pageNumber, filter
        this.filter = filter;

        // Load dữ liệu
        await this.loadData();
      } catch (ex) {
        console.log(ex);
      }
    },
    /**
     * Click Sửa nhân viên
     * @param employeeId: Id nhân viên cần sửa
     * @author LNTGiang
     */
    editEmployee(employeeId) {
      try {
        // Gán trạng thái data là sửa
        this.eStatus = Constant.DataStatus.Edit;

        // Mở Dialog nhân viên
        this.isShowDialog = true;

        // Gán mã nhân viên đã chọn
        this.employeeIdSelected = employeeId;
      } catch (ex) {
        console.log(ex);
      }
    },

    /**
     * Xóa nhân viên
     * @author LNTGiang
     */
    async deleteEmployee() {
      // Thực hiện gọi api xóa nhân viên
      try {
        let response = await EmployeeRepository.deleteEmployee([
          this.employeeIdSelected,
        ]);
        switch (response.status) {
          case 201:
            this.showToast(
              ResourceVN.DeleteSuccess,
              Constant.ToastStatus.Success
            );
            break;
          case 200:
            break;
        }

        // Khi xóa thành công thì load lại data
        await this.loadData();
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Gọi api xuất file
     * @author LNTGiang
     */
    exportFile() {
      try {
        window.open(ApiUrl.Employee.Export);
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Xóa nhiều nhân viên
     * @author LNTGiang
     */
    async multiDeleteEmployee() {
      // Show loading
      this.isShowLoading = true;
      try {
        let response = await EmployeeRepository.deleteEmployee(
          this.employeeIds
        );
        switch (response.status) {
          case 200:
            break;
          case 201:
            this.showToast(
              ResourceVN.DeleteSuccess,
              Constant.ToastStatus.Success
            );
            break;
        }
        this.loadData();
      } catch (error) {
        console.log(error);
      }

      // Đóng Loading
      this.isShowLoading = false;
    },
    /**
     * Nhân bản nhân viên
     * @param employeeId: id nhân viên cần nhân bản
     * @author LNTGiang
     */
    replicationEmployee(employeeId) {
      try {
        // Gán trạng thái data là nhân bản
        this.eStatus = Constant.DataStatus.Replication;

        this.employeeIdSelected = employeeId;

        // Mở Dialog nhân viên
        this.isShowDialog = true;
      } catch (ex) {
        console.log(ex);
      }
    },
  },
};
</script>

<style scoped>
@import url("@/style/page/employee.css");
</style>
