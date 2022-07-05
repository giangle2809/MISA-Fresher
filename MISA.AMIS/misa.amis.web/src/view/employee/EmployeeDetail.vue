<template>
  <ms-dialog :isShow="isShowDialog" @closeDialog="close">
    <div class="ms-el-header">
      <div class="ms-title">Thông tin nhân viên</div>
      <div class="px-6">
        <ms-checkbox>Là khách hàng</ms-checkbox>
      </div>
      <div class="px-6">
        <ms-checkbox>Là nhà cung cấp</ms-checkbox>
      </div>
    </div>
    <div class="ms-el-container">
      <div class="ms-el-content">
        <div class="flex pd-b-12">
          <div class="col-5 pd-r-26">
            <div class="row pd-b-12">
              <div class="col-4 pd-r-6">
                <div class="ms-label-input flex">
                  Mã
                  <div class="ms-input-required">&nbsp;*</div>
                </div>
                <ms-input
                  :value="employee.EmployeeCode"
                  :type="'text'"
                  :autoFocus="autoFocus['EmployeeCode']"
                  :required="true"
                  :maxlength="20"
                  :titleError="resource.EmployeeCodeEmpty"
                  :tabindex="1"
                  @onKeyup="
                    (value) => {
                      dataChange('EmployeeCode', value);
                    }
                  "
                ></ms-input>
              </div>
              <div class="col-6">
                <div class="ms-label-input flex">
                  Tên
                  <div class="ms-input-required">&nbsp;*</div>
                </div>
                <ms-input
                  :value="employee.FullName"
                  :required="true"
                  :maxlength="100"
                  :autoFocus="autoFocus['FullName']"
                  :titleError="resource.EmployeeNameEmpty"
                  :tabindex="2"
                  @onKeyup="
                    (value) => {
                      dataChange('FullName', value);
                    }
                  "
                  :type="'text'"
                ></ms-input>
              </div>
            </div>
            <div class="pd-b-12">
              <div class="ms-label-input flex">
                Đơn vị
                <div class="ms-input-required">&nbsp;*</div>
              </div>
              <ms-combobox
                :value="employee.DepartmentName"
                :required="true"
                :autoFocus="autoFocus['DepartmentName']"
                :tabindex="3"
                @onKeyup="searchDepartment"
                @onBlur="blurCombobox"
              >
                <div class="ms-department-item-header flex">
                  <div style="text-align: left; width: 100px; padding: 0 10px">
                    Mã đơn vị
                  </div>
                  <div style="text-align: left; width: 100%; padding: 0 10px">
                    Tên đơn vị
                  </div>
                </div>
                <div
                  class="ms-department-item flex"
                  :class="{
                    'ms-department-item-active':
                      item.DepartmentId == employee.DepartmentId,
                  }"
                  v-for="(item, index) in departmentsDisplay"
                  :key="index"
                  @click="departmentOnClick(item)"
                >
                  <div style="text-align: left; width: 100px; padding: 0 10px">
                    {{ item.DepartmentCode }}
                  </div>
                  <div style="text-align: left; width: 100%; padding: 0 10px">
                    {{ item.DepartmentName }}
                  </div>
                </div>
              </ms-combobox>
            </div>
            <div class="pd-b-12">
              <div class="ms-label-input">Chức danh</div>
              <ms-input
                :value="employee.PositionName"
                :maxlength="100"
                :autoFocus="autoFocus['PositionName']"
                :tabindex="4"
                @onKeyup="
                  (value) => {
                    dataChange('PositionName', value);
                  }
                "
                :type="'text'"
              ></ms-input>
            </div>
          </div>
          <div class="col-5">
            <div class="row pd-b-12">
              <div class="col-4 pd-r-6">
                <div class="ms-label-input">Ngày sinh</div>
                <ms-input
                  :dateValue="employee.DateOfBirth"
                  :type="'date'"
                  :tabindex="5"
                  :autoFocus="autoFocus['DateOfBirth']"
                  @onChange="
                    (value) => {
                      dataChange('DateOfBirth', value);
                    }
                  "
                ></ms-input>
              </div>
              <div class="col-6">
                <div class="ms-label-input">Giới tính</div>
                <div class="row pd-t-5">
                  <ms-radio
                    :checked="employee?.Gender == gender?.Male"
                    :radioName="'gender'"
                    :tabindex="6"
                    @onChange="dataChange('Gender', gender?.Male)"
                    >Nam</ms-radio
                  >
                  <ms-radio
                    :checked="employee?.Gender == gender?.Female"
                    :radioName="'gender'"
                    :tabindex="7"
                    @onChange="dataChange('Gender', gender?.Female)"
                    >Nữ</ms-radio
                  >
                  <ms-radio
                    :checked="employee?.Gender == gender?.Other"
                    :radioName="'gender'"
                    :tabindex="8"
                    @onChange="dataChange('Gender', gender?.Other)"
                    >Khác</ms-radio
                  >
                </div>
              </div>
            </div>
            <div class="row pd-b-12">
              <div class="col-6 pd-r-6">
                <div class="ms-label-input">Số CMND</div>
                <ms-input
                  :value="employee.IdentityNumber"
                  :type="'text'"
                  :maxlength="25"
                  :tabindex="9"
                  @onKeyup="
                    (value) => {
                      dataChange('IdentityNumber', value);
                    }
                  "
                ></ms-input>
              </div>
              <div class="col-4">
                <div class="ms-label-input">Ngày cấp</div>
                <ms-input
                  :dateValue="employee.IdentityDate"
                  :type="'date'"
                  :tabindex="10"
                  :autoFocus="autoFocus['IdentityDate']"
                  @onChange="
                    (value) => {
                      dataChange('IdentityDate', value);
                    }
                  "
                ></ms-input>
              </div>
            </div>
            <div class="pd-b-12">
              <div class="ms-label-input">Nơi cấp</div>
              <ms-input
                :value="employee.IdentityPlace"
                :maxlength="255"
                :tabindex="11"
                @onKeyup="
                  (value) => {
                    dataChange('IdentityPlace', value);
                  }
                "
                :type="'text'"
              ></ms-input>
            </div>
          </div>
        </div>
      </div>
      <div class="pd-b-12">
        <div class="ms-label-input">Địa chỉ</div>
        <ms-input
          :value="employee.Address"
          :maxlength="255"
          :tabindex="12"
          @onKeyup="
            (value) => {
              dataChange('Address', value);
            }
          "
          :type="'text'"
        ></ms-input>
      </div>
      <div class="row pd-b-12">
        <div class="col-25 pd-r-6">
          <div class="ms-label-input">ĐT di động</div>
          <ms-input
            :value="employee.PhoneNumber"
            :maxlength="50"
            :tabindex="13"
            @onKeyup="
              (value) => {
                dataChange('PhoneNumber', value);
              }
            "
            :type="'text'"
          ></ms-input>
        </div>
        <div class="col-25 pd-r-6">
          <div class="ms-label-input">ĐT cố định</div>
          <ms-input
            :value="employee.TelephoneNumber"
            :maxlength="50"
            :tabindex="14"
            @onKeyup="
              (value) => {
                dataChange('TelephoneNumber', value);
              }
            "
            :type="'text'"
          ></ms-input>
        </div>
        <div class="col-25 pd-r-6">
          <div class="ms-label-input">Email</div>
          <ms-input
            :value="employee.Email"
            :maxlength="100"
            :tabindex="15"
            :autoFocus="autoFocus['Email']"
            @onKeyup="
              (value) => {
                dataChange('Email', value);
              }
            "
            :type="'text'"
          ></ms-input>
        </div>
      </div>
      <div class="row pd-b-12">
        <div class="col-25 pd-r-6">
          <div class="ms-label-input">Tài khoản ngân hàng</div>
          <ms-input
            :value="employee.BankAccountNumber"
            :maxlength="25"
            :tabindex="16"
            @onKeyup="
              (value) => {
                dataChange('BankAccountNumber', value);
              }
            "
            :type="'text'"
          ></ms-input>
        </div>
        <div class="col-25 pd-r-6">
          <div class="ms-label-input">Tên ngân hàng</div>
          <ms-input
            :value="employee.BankName"
            :maxlength="255"
            :tabindex="17"
            @onKeyup="
              (value) => {
                dataChange('BankName', value);
              }
            "
            :type="'text'"
          ></ms-input>
        </div>
        <div class="col-25 pd-r-6">
          <div class="ms-label-input">Chi nhánh</div>
          <ms-input
            :value="employee.BankBranchName"
            :maxlength="255"
            :tabindex="18"
            @onKeyup="
              (value) => {
                dataChange('BankBranchName', value);
              }
            "
            :type="'text'"
          ></ms-input>
        </div>
      </div>
      <div class="ms-el-foot">
        <div class="devide"></div>
        <div class="flex">
          <ms-button
            @click="$emit('close')"
            :text="'Hủy'"
            :classList="'ms-btn-secondary'"
          >
          </ms-button>
          <div class="ms-space"></div>
          <ms-button
            @click="btnSaveOnClick(false)"
            :title="'Cất(Ctrl + S)'"
            :text="'Cất'"
            :classList="'ms-btn-secondary'"
          >
          </ms-button>
          <div style="margin: 5px"></div>
          <ms-button
            :classList="'ms-btn-success'"
            @click="btnSaveOnClick(true)"
            :title="'Cất và Thêm(Ctrl + Shift + S)'"
            :text="'Cất và Thêm'"
          >
          </ms-button>
        </div>
      </div>
    </div>
  </ms-dialog>
  <ms-popup-message
    :isShow="isShowMessage"
    :message="message"
    :status="statusMess"
  >
    <ms-button
      :text="'Hủy'"
      @click="closePopUpMessage"
      v-if="statusMess === messageBoxStatus.Question"
    />
    <div class="ms-space"></div>
    <ms-button
      :text="'Đóng'"
      @click="closePopUpMessage"
      :classList="'ms-btn-success'"
      v-if="statusMess === messageBoxStatus.Error"
    />
    <div class="ms-space"></div>
    <ms-button
      :text="'Đồng ý'"
      :classList="'ms-btn-success'"
      @click="closePopUpMessage"
      v-if="statusMess === messageBoxStatus.Warn"
    />
    <ms-button
      :text="'Không'"
      style="margin-right: 10px"
      @click="responseMessage(false)"
      v-if="statusMess === messageBoxStatus.Question"
    />
    <ms-button
      :text="'Có'"
      :classList="'ms-btn-success'"
      @click="responseMessage(true)"
      v-if="statusMess === messageBoxStatus.Question"
    />
  </ms-popup-message>
  <ms-toast
    :mess="toastContent"
    :isShow="isShowToast"
    :status="statusToast"
    @close="closeToast"
  />
  <ms-loading :isShow="isShowLoading"></ms-loading>
</template>

<script>
import Constant from "@/utils/constant";
import ResourceVN from "@/utils/resourceVN";
import EmployeeRepository from "@/repository/employeeRepository";
import DepartmentRepository from "@/repository/departmentRepository";

export default {
  data() {
    return {
      gender: Constant.Gender,
      resource: ResourceVN,
      messageBoxStatus: Constant.MessageBoxStatus,
      isDataChange: false,
      employee: {},
      isShowMessage: false,
      message: "",
      status: Constant.DataStatus.Add,
      statusMess: "",
      departments: [],
      departmentsDisplay: [],
      inputFocus: "",
      autoFocus: {},
      isShowLoading: false,
      isShowToast: false,
      toastContent: "",
      statusToast: "",
      timeout: null,
    };
  },
  emits: ["close", "refresh"],
  mounted() {
    document.addEventListener("keydown", this.onKeyDown);
    this.loadDepartments();
  },
  unmounted() {
    document.removeEventListener("keydown", this.onKeyDown);
  },
  props: ["eStatus", "employeeSelected", "isShowDialog"],
  watch: {
    /**
     * auto focus ô input đầu tiên
     */
    async isShowDialog(value) {
      this.autoFocus["EmployeeCode"] = value;
      this.isDataChange = false;
      this.employee = {};
      if (value) {
        if (
          this.eStatus == Constant.DataStatus.Edit ||
          this.eStatus == Constant.DataStatus.Replication
        ){
          await this.loadEmployeeData();
        }
        if (
          this.eStatus == Constant.DataStatus.Replication ||
          this.eStatus == Constant.DataStatus.Add
        ){
          await this.loadNewEmployeeCode();
          }
        if(this.eStatus === Constant.DataStatus.Add){
          this.employee["FullName"] = "Lê Ngọc Trường Giang";
          this.employee["Gender"] = Constant.Gender.Male;
        }
      }
      this.departmentsDisplay = this.departments;
    },
  },
  methods: {
    /**
     * Sự kiện khi ấn các phím tắt
     * @author LNTGiang
     */
    onKeyDown(event) {
      if (this.isShowDialog && !this.isShowLoading) {
        // Ctrl + Shift + S => Cất và thêm
        if (event.ctrlKey && event.shiftKey && event.code == "KeyS") {
          event.preventDefault();
          this.btnSaveOnClick(true);
          return;
        }
        // Ctrl + S => Cất
        if (event.ctrlKey && event.code == "KeyS") {
          event.preventDefault();
          this.btnSaveOnClick(false);
          return;
        }
        // Esc => Đóng Dialog
        if (event.key == "Escape") {
          event.preventDefault();
          this.close();
        }
      }
    },

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
     * @param message: Nội dung của MessageBox
     * @author LNTGiang
     */
    showPopUpMessage(statusMess, message) {
      this.isShowMessage = true;
      this.statusMess = statusMess;
      this.message = message;
    },

    /**
     * Đóng MessageBox
     * @author LNTGiang
     */
    closePopUpMessage() {
      if (
        this.statusMess === Constant.MessageBoxStatus.Error ||
        this.statusMess === Constant.MessageBoxStatus.Warn
      ) {
        this.autoFocus[this.inputFocus] = true;
      }
      this.isShowMessage = false;
    },

    /**
     * Đóng Dialog
     * @author LNTGiang
     */
    close() {
      // Kiểm tra data có thay đổi không
      if (this.isDataChange) {
        this.showPopUpMessage(
          Constant.MessageBoxStatus.Question,
          ResourceVN.EmployeeDataChange
        );
      } else {
        this.$emit("close");
        this.refreshData();
      }
    },

    /**
     * Hàm Validate dữ liệu
     * @author LNTGiang
     */
    validateData() {
      this.autoFocus = {};
      if (this.employee) {
        // Kiểm tra mã nhân viên không được phép bỏ trống
        if (!this.employee.EmployeeCode || this.employee.EmployeeCode == "") {
          this.inputFocus = "EmployeeCode";
          this.showPopUpMessage(
            Constant.MessageBoxStatus.Error,
            ResourceVN.EmployeeCodeEmpty
          );
          return false;
        }

        // Kiểm tra tên nhân viên không được phép bỏ trống
        if (!this.employee.FullName || this.employee.FullName == "") {
          this.inputFocus = "FullName";
          this.showPopUpMessage(
            Constant.MessageBoxStatus.Error,
            ResourceVN.EmployeeNameEmpty
          );
          return false;
        }

        // Kiểm tra đơn vị không được phép bỏ trống
        if (!this.employee.DepartmentId || this.employee.DepartmentId == "") {
          this.inputFocus = "DepartmentName";
          this.showPopUpMessage(
            Constant.MessageBoxStatus.Error,
            ResourceVN.DepartmentEmpty
          );
          return false;
        }

        // Kiểm tra ngày sinh không được lớn hơn ngày hiện tại
        if (
          this.employee.DateOfBirth &&
          this.employee.DateOfBirth > new Date()
        ) {
          this.inputFocus = "DateOfBirth";
          this.showPopUpMessage(
            Constant.MessageBoxStatus.Error,
            ResourceVN.DateOfBirthValidate
          );
          return false;
        }

        // Kiểm tra ngày cấp không được lớn hơn ngày hiện tại
        if (
          this.employee.IdentityDate != null &&
          this.employee.IdentityDate > new Date()
        ) {
          this.inputFocus = "IdentityDate";
          this.showPopUpMessage(
            Constant.MessageBoxStatus.Error,
            ResourceVN.IdentityDateValidate
          );
          return false;
        }

        // Kiểm tra email đúng định dạng không
        let mailFormat = /^[a-zA-Z0-9_\\.]+\.*@[a-zA-z0-9]+(\.).+$/;
        if (
          (this.employee.Email && this.employee.Email != "") &&
          !this.employee.Email.match(mailFormat)
        ) {
          this.inputFocus = "Email";
          this.showPopUpMessage(
            Constant.MessageBoxStatus.Error,
            ResourceVN.EmailValidate
          );
          return false;
        }
      } else {
        return false;
      }
      return true;
    },

    /**
     * Phản hồi khi được hỏi cất thông tin
     * @param value: Giá trị khi ấn có hoặc không
     * @author LNTGiang
     */
    async responseMessage(value) {
      try {
        this.closePopUpMessage();
        if (value) {
          // Lưu giữ liệu
          await this.btnSaveOnClick(false);
        }
        // Đóng Dialog
        this.$emit("close");
      } catch (ex) {
        console.log(ex);
      }
    },

    /**
     * Khi chọn 1 phòng ban
     * @param value: Giá trị khi chọn
     * @author LNTGiang
     */
    departmentOnClick(value) {
      try {
        if (!value) {
          this.employee.DepartmentId = "";
          this.employee.DepartmentName = "";
        } else {
          this.employee.DepartmentId = value.DepartmentId;
          this.employee.DepartmentName = value.DepartmentName;
        }
        this.isDataChange = true;
        this.departmentsDisplay = this.departments;
      } catch (ex) {
        console.log(ex);
      }
    },

    /**
     * thêm, sửa dữ liệu
     * @param isAdd: sau khi lưu có chuyển sang trạng thái thêm mới không
     * @author LNTGiang
     */
    async btnSaveOnClick(isAdd) {
      let err = false;
      let toastContent = "";
      // Thực hiện validate
      if (this.validateData()) {
        // Show loading
        this.isShowLoading = true;
        try {
          let response = null;
          // Thêm nhân viên
          if (
            this.eStatus == Constant.DataStatus.Add ||
            this.eStatus == Constant.DataStatus.Replication
          ) {
            response = await EmployeeRepository.insertEmployee(this.employee);
            toastContent = ResourceVN.AddSuccess;
          }
          // Sửa nhân viên
          else if (this.eStatus == Constant.DataStatus.Edit) {
            response = await EmployeeRepository.updateEmployee(
              this.employeeSelected,
              this.employee
            );
            toastContent = ResourceVN.EditSuccess;
          }
          switch (response.status) {
            case 200:
              break;
            case 201:
              this.showToast(toastContent, Constant.ToastStatus.Success);
              break;
          }
        } catch (error) {
          let message;
          if (error) {
            message = error.response.data.UserMsg;
            switch (error.response.status) {
              case 400:
                this.inputFocus = error.response.data.ErrorCode;
                break;
              default:
                break;
            }
          }
          this.showPopUpMessage(Constant.MessageBoxStatus.Warn, message);
          err = true;
        }
      } else {
        err = true;
      }
      this.isShowLoading = false;
      if (err) {
        return;
      }

      // Nếu click vào nút cất và thêm
      if (isAdd) {
        // Xóa hết dữ liệu
        this.employee = {};
        this.loadNewEmployeeCode();
        // Chuyển trạng thái về thêm mới
        this.status = Constant.DataStatus.Add;
        this.isDataChange = false;
        this.autoFocus['EmployeeCode'] = true;
      } else {
        // Đóng dialog
        this.$emit("close");
        // Load lại data ở table
        this.refreshData();
      }
    },

    /**
     * Sau khi các input nhập xong thì gán ngược giá trị
     * @param key: key cần gán giá trị
     * @param value: Giá trị được gán
     * @author LNTGiang
     */
    dataChange(key, value) {
      try {
        this.employee[key] = value;
        this.isDataChange = true;
      } catch (ex) {
        console.log(ex);
      }
    },

    /**
     * Refresh lại data khi update hoặc edit thành công
     * @author LNTGiang
     */
    refreshData() {
      this.$emit("refresh");
    },

    /**
     * gọi api load nhân viên theo id nhân viên
     * @author LNTGiang
     */
    async loadEmployeeData() {
      this.isShowLoading = true;
      try {
        let response = await EmployeeRepository.getById(this.employeeSelected);
        if (response) {
          this.employee = response.data;
        }
      } catch (error) {
        console.log(error);
      }
      this.isShowLoading = false;
    },

    /**
     * gọi api lấy danh sách phòng ban
     * @author LNTGiang
     */
    async loadDepartments() {
      try {
        let response = await DepartmentRepository.getAll();
        if (response) {
          this.departments = response.data;
        }
      } catch (ex) {
        console.log(ex);
      }
    },

    /**
     * gọi api lấy mã nhân viên mới
     * @author LNTGiang
     */
    async loadNewEmployeeCode() {
      this.isShowLoading = true;
      try {
        let response = await EmployeeRepository.getNemEmployeeCode();
        if (this.employee && response) {
          this.employee["EmployeeCode"] = response.data;
        }
      } catch (ex) {
        console.log(ex);
      }
      this.isShowLoading = false;
    },

    /**
     * Mô tả code
     * @param value: Giá trị của input khi nhập
     * @author: LNTGiang
     */
    searchDepartment(value) {
      try {
        if (this.timeout) {
          clearTimeout(this.timeout);
        }
        this.employee.DepartmentName = value;
        this.timeout = setTimeout(() => {
          if (!value) {
            value = "";
          }
          this.departmentsDisplay = [];
          if (this.departments) {
            this.departments.forEach((element) => {
              if (element.DepartmentName.includes(value)) {
                this.departmentsDisplay.push(element);
              }
            });
          }
        }, 500);
      } catch (ex) {
        console.log(ex);
      }
    },

    /**
     * Khi không focus vào
     * @author LNTGiang
     */
    blurCombobox() {
      try {
        this.departmentsDisplay.forEach((element) => {
          if (
            element.DepartmentName === this.employee.DepartmentName &&
            element.DepartmentId === this.employee.DepartmentId
          ) {
            return;
          }
        });
        if (this.departmentsDisplay && this.departmentsDisplay.length > 0) {
          this.departmentOnClick(this.departmentsDisplay[0]);
        } else {
          this.departmentOnClick(null);
        }
      } catch (ex) {
        console.log(ex);
      }
    },
  },
};
</script>

<style scoped>
@import url("@/style/page/employee-detail.css");
</style>
