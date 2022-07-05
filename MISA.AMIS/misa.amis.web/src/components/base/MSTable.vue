<template>
  <div
    class="ms-batch-excution-expand"
    style="display: none"
    ref="excution-expand"
  >
    <div class="ms-batch-excution-item pointer" @click="btnMultiDeleteOnClick">
      Xóa
    </div>
  </div>
  <div class="ms-gird">
    <div class="ms-filter">
      <div>
        <div
          class="ms-batch-excution"
          ref="batch-excution"
          @click="batchExcutionOnClick($event)"
        >
          <div class="ms-batch-excution-text">Thực hiện hàng loạt</div>
          <div class="ms ms-icon-16 ms-icon-arrow-dropdown"></div>
        </div>
      </div>
      <div class="ms-space"></div>
      <ms-input
        type="text"
        placehoder="Tìm theo mã, tên nhân viên"
        :value="filter.keyWord"
        :isIcon="true"
        @onKeyup="keyup"
        style="padding-right: 6px"
      >
        <template #icon>
          <div class="ms-icon-input ms-icon-16 ms ms-icon-search"></div>
        </template>
      </ms-input>
      <div
        class="ms-icon-24 ms ms-icon-refresh ms-table-list-btn"
        @click="btnRefreshOnClick"
      ></div>

      <div
        class="ms-icon-24 ms ms-icon-excel_nav ms-table-list-btn"
        @click="btnExportOnClick"
      ></div>
    </div>
    <div class="ms-table-box">
      <div class="ms-table">
        <table>
          <thead>
            <tr>
              <th class="ms-pd-left"></th>
              <th
                class="text-align-center ms-tb-cb"
                style="left: 30px !important; z-index: 100"
              >
                <ms-checkbox
                  :checked="headerChecked"
                  @onChange="headerCheckedOnClick"
                ></ms-checkbox>
              </th>

              <th v-for="(item, index) in tableConfig" :key="index">
                {{ item.columnName }}
              </th>
              <th
                class=""
                style="
                  border-left: 1px solid #c7c7c7;
                  right: 30px !important;
                  z-index: 100;
                "
              >
                CHỨC NĂNG
              </th>
              <th class="ms-pd-right"></th>
              <th class="ms-out-right"></th>
            </tr>
          </thead>
          <tbody>
            <tr
              v-for="(item, index) in data"
              :key="index"
              @dblclick="btnEditOnClick(item[idCode])"
            >
              <td class="ms-pd-left"></td>
              <td
                class="text-align-center ms-tb-cb sticky"
                style="left: 30px !important; z-index: 99"
              >
                <ms-checkbox
                  :checked="checks[index]"
                  @onChange="
                    (value) => checkedOnClick(value, item[idCode], index)
                  "
                ></ms-checkbox>
              </td>

              <td
                v-for="(column, index) in tableConfig"
                :key="index"
                class="ms-td-view"
              >
                {{ item[column.columnCode] }}
              </td>
              <td
                class="sticky text-align-center ms-td-view"
                style="
                  border-left: 1px solid #c7c7c7;
                  right: 30px !important;
                  z-index: 99;
                "
              >
                <div class="ms-function-box">
                  <div
                    class="ms-function-text"
                    @click="btnEditOnClick(item[idCode])"
                  >
                    Sửa
                  </div>
                  <div
                    class="ms-function-icon"
                    @click="
                      btnExpandOnClick(
                        item[idCode],
                        item[tableConfig[0].columnCode],
                        $event
                      )
                    "
                  >
                    <div
                      class="ms-icon-16 ms ms-icon-arrow-dropdown-blue"
                    ></div>
                  </div>
                </div>
              </td>
              <td class="ms-pd-right"></td>
              <td class="ms-out-right"></td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
    <div class="ms-navi">
      <div class="ms-navi-sumary">
        Tổng số: <b>{{ totalRecord }}</b> bản ghi
      </div>
      <div class="ms-space"></div>
      <div class="ms-record-in-page">
        <ms-combobox
          :value="`${filter.pageSize} bản ghi trên 1 trang`"
          :disable="true"
          :showInBottom="true"
        >
          <div
            class="ms-page-size-item"
            v-for="(item, index) in dataPageSizes"
            :key="index"
            :class="{ 'ms-page-size-item-active': filter.pageSize == item }"
            @click="btnPageSizeOnSelect(item)"
          >
            {{ item }}&nbsp;bản ghi trên 1 trang
          </div>
        </ms-combobox>
      </div>
      <div
        class="btn-pre pointer"
        :class="{ 'disable-btn': isPrevDisable }"
        @click="btnPageOnClick($event, 1, paginationStatus.prev)"
      >
        Trước
      </div>
      <div
        class="navi-btn-number pointer"
        :class="{
          'navi-btn-number-active': item == filter['pageNumber'],
          'disable-btn': item == '...',
        }"
        v-for="(item, index) in pages"
        :key="index"
        @click="btnPageOnClick($event, item, paginationStatus.number)"
      >
        {{ item }}
      </div>
      <div
        class="btn-next pointer"
        :class="{ 'disable-btn': isNextDisable }"
        @click="btnPageOnClick($event, 1, paginationStatus.next)"
      >
        Sau
      </div>
    </div>
  </div>
  <div class="ms-function-expand" ref="expand" style="display: none">
    <div class="ms-expand-item" @click="btnReplicationOnClick">Nhân bản</div>
    <div class="ms-expand-item" @click="btnDeleteOnClick">Xóa</div>
    <div class="ms-expand-item">Ngưng sử dụng</div>
  </div>
</template>

<script>
import Constant from "@/utils/constant";
export default {
  name: "MSTable",
  emits: [
    "refresh",
    "edit",
    "delete",
    "multiDelete",
    "search",
    "export",
    "replication",
  ],
  data() {
    return {
      paginationStatus: Constant.PaginationStatus,
      idSelected: "",
      codeSelected: "",
      filter: {
        pageSize: 20,
        pageNumber: 1,
        keyWord: "",
      },
      dataPageSizes: [10, 20, 30, 50, 100],
      checks: new Array(100),
      headerChecked: false,
      idsChecked: [],
      timeout: null
    };
  },
  mounted() {
    document.addEventListener("click", this.documentOnClick);
  },
  unmounted() {
    document.removeEventListener("click", this.documentOnClick);
  },
  props: ["data", "totalRecord", "totalPage", "tableConfig", "idCode"],
  watch: {
    /**
     * Khi thay đổi data
     */
    data() {
      this.checks.fill(false);
      this.idsChecked = [];
    },
  },
  computed: {
    /**
     * disable nút prev
     * @author LNTGiang
     */
    isPrevDisable: function () {
      if (!this.filter) {
        return true;
      }
      return this.filter["pageNumber"] <= 1;
    },
    /**
     * disable nút next
     * @author LNTGiang
     */
    isNextDisable: function () {
      if (!this.filter) {
        return true;
      }
      return this.filter["pageNumber"] >= this.totalPage;
    },
    /**
     * Load lại pagination
     * @author LNTGiang
     */
    pages: function () {
      let pages = [1];
      if (this.filter) {
        let pageNumber = this.filter["pageNumber"];

        if (this.totalPage > 4) {
          if (pageNumber > 2) {
            pages.push("...");
          }
          let page;
          if (pageNumber < this.totalPage - 1) {
            for (
              page = pageNumber > 1 ? pageNumber : 2;
              page < pageNumber + 3 && page < this.totalPage;
              page++
            ) {
              pages.push(page);
            }
            if (this.totalPage > page) pages.push("...");
          } else {
            pages.push(this.totalPage - 2);
            pages.push(this.totalPage - 1);
          }
          pages.push(this.totalPage);
        } else {
          for (let i = 2; i <= this.totalPage; i++) {
            pages.push(i);
          }
        }
      }
      return pages;
    },
  },
  methods: {
    /**
     * Click checkbox trên thead
     * @author LNTGiang
     */
    headerCheckedOnClick(value) {
      this.headerChecked = value;
      if (value) {
        this.checks.fill(true);
        this.idsChecked = [];
        this.data.forEach((element) => {
          this.idsChecked.push(element[this.idCode]);
        });
      }
    },
    /**
     * Click checkbox ở tbody
     * @author LNTGiang
     */
    checkedOnClick(value, id, i) {
      this.checks[i] = value;
      if (value) {
        this.idsChecked.push(id);
      } else {
        var index = this.idsChecked.indexOf(id);
        if (index > -1) {
          this.idsChecked.splice(index, 1);
        }
      }
    },
    /**
     * Sự kiện click ra ngoài đóng expand
     * @author LNTGiang
     */
    documentOnClick(event) {
      if (!event.target.classList.contains("ms-icon-arrow-dropdown-blue"))
        this.$refs["expand"].style.display = "none";

      if (!this.$refs["batch-excution"].contains(event.target))
        this.$refs["excution-expand"].style.display = "none";
    },
    /**
     * Sự kiện ấn mở rộng ở row trong cột chức năng
     * @author LNTGiang
     */
    btnExpandOnClick(id, code, event) {
      let display = this.$refs["expand"].style.display;
      if (display === "none") {
        let left = event.target.getBoundingClientRect().left;
        let top = event.target.getBoundingClientRect().top;
        this.$refs["expand"].style.left = `${left - 80}px`;
        this.$refs["expand"].style.top = `${top + 20}px`;
        this.$refs["expand"].style.display = "block";
      } else {
        this.$refs["expand"].style.display = "none";
      }
      this.idSelected = id;
      this.codeSelected = code;
    },
    /**
     * Sự kiện ấn thực hiện hàng loạt
     * @author LNTGiang
     */
    batchExcutionOnClick(event) {
      // Nếu chưa chọn hoặc chọn 1 phần tử thì không hiển thị
      if (this.idsChecked && this.idsChecked.length < 1) return;
      let display = this.$refs["excution-expand"].style.display;
      if (display === "none") {
        let top = event.target.getBoundingClientRect().top;
        let right = event.target.getBoundingClientRect().right;
        this.$refs["excution-expand"].style.top = `${top + 32}px`;
        this.$refs["excution-expand"].style.right = `calc(100% - ${right}px)`;
        this.$refs["excution-expand"].style.display = "block";
      } else {
        this.$refs["excution-expand"].style.display = "none";
      }
    },
    /**
     * Sự kiện khi click button refresh để load lại dữ liệu
     * @author LNTGiang
     */
    btnRefreshOnClick() {
      this.filter.pageNumber = 1;
      this.filter.keyWord = "";
      this.$emit("refresh");
    },
    /**
     * Click vào nút xóa nhiều
     * @author LNTGiang
     */
    btnMultiDeleteOnClick() {
      this.$emit("multiDelete", this.idsChecked);
    },
    /**
     * Sự kiện keyup khi gõ ở mục tìm kiếm sau 0.5s từ lần gõ cuối cùng thì sẽ gọi api tìm kiếm
     * @author LNTGiang
     */
    keyup(value) {
      if (this.timeout) {
        clearTimeout(this.timeout);
      }
      this.timeout = setTimeout(() => {
        this.filter["filter"] = value;
        this.filter["pageNumber"] = 1;
        this.search();
      }, 500);
      
    },
    /**
     * Sự kiện click vào button edit
     * @param (Object) value: đối tượng cần sửa
     * @author LNTGiang
     */
    btnEditOnClick(value) {
      this.$emit("edit", value);
    },
    /**
     * Sự kiện click vào button xóa
     * @author LNTGiang
     */
    btnDeleteOnClick() {
      this.$emit("delete", this.idSelected, this.codeSelected);
    },
    /**
     *
     */
    btnReplicationOnClick() {
      this.$emit("replication", this.idSelected);
    },
    /**
     * Sự kiện click vào button export
     */
    btnExportOnClick() {
      this.$emit("export");
    },
    /**
     * Sự kiện click vào page ở pagination để load trang khác
     * @author LNTGiang
     */
    btnPageOnClick(event, value, status) {
      if (event.target.classList.contains("disable-btn")){
        return;
      } 
      // Nếu ấn nút trở về trang trước
      if (status == this.paginationStatus.prev) {
        this.filter["pageNumber"]--;
      }
      // Nếu ấn nút sang trang kế tiếp
      else if (status == this.paginationStatus.next) {
        this.filter["pageNumber"]++;
      }
      // Nếu chọn 1 trang cụ thể
      else if (status == this.paginationStatus.number) {
        this.filter["pageNumber"] = value;
      }

      // Load data
      this.search();
    },
    /**
     * Khi chọn PageSize
     * @author LNTGiang
     */
    btnPageSizeOnSelect(value) {
      this.filter["pageSize"] = value;
      this.filter["pageNumber"] = 1;
      this.search();
    },
    /**
     * Tìm kiếm
     * @author LNTGiang
     */
    search() {
      this.$emit("search", this.filter);
    },
  },
};
</script>

<style scoped>
@import url("@/style/components/table.css");
</style>
