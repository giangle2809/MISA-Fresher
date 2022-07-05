<template>
  <div class="m-gird">
    <div class="m-gird-button">
      <ms-button :icon="'mi mi-16 mi-document-add'" @click="btnAddOnClick"
        >Thêm</ms-button
      >
      <ms-button :icon="'mi mi-16 mi-document-clone'" @click="btnCloneOnClick"
        >Nhân bản</ms-button
      >
      <ms-button :icon="'mi mi-16 mi-document-edit'" @click="btnEditOnClick"
        >Sửa</ms-button
      >
      <ms-button :icon="'mi mi-16 mi-document-delete'" @click="btnDeleteOnClick"
        >Xóa</ms-button
      >
      <ms-button
        :icon="'mi mi-16 mi-document-refresh'"
        @click="btnRefreshOnClick"
        >Nạp</ms-button
      >
    </div>
    <div class="m-table">
      <table>
        <thead>
          <tr>
            <td v-for="(head, index) in tableConfig.header" :key="index">
              <div>
                <div style="text-align: center">
                  {{ head.columnDisplayName }}
                </div>
                <div class="m-filter-box" v-if="head.compare != 'option'">
                  <button style="cursor: pointer" @click="expandFilter(index)">
                    *
                  </button>
                  <input
                    style="width: calc(100% - 28px)"
                    :type="head.compare"
                    v-model="filters[index].Value"
                    @keyup="inputChange"
                  />
                  <div
                    class="m-expand-filter"
                    :style="[
                      isShowExpand[index] ? 'display:block' : 'display:none',
                    ]"
                  >
                  <template v-for="(item, index1) in filterOption"
                      :key="index1">
                    <div
                      class="m-expand-filter-item"
                      v-if="item.compare.includes(head.compare)"
                      :class="{
                        'm-expand-filter-item-active':
                          filters[index].Calculation === item.value,
                      }"
                      @click="filterOptionChange(index, item)"
                    >
                      {{ item.display }}
                    </div>
                  </template>
                  </div>
                </div>
                <div
                  class="m-filter-box"
                  style="border: none"
                  v-if="head.compare == 'option'"
                >
                  <ms-select
                    :value="filters[index].Display"
                    :disable="true"
                    :isClear="true"
                    style="background-color: #fff"
                    @clear="clearSelectOption(index)"
                  >
                    <div 
                      class="ms-select-item" 
                      v-for="(item, index1) in head.data " :key="index1"
                      :class="{'ms-select-active': item.Value == filters[index].Value}"
                      @click="filterSelectOptionOnClick(index, item)"
                      >{{item.Name}}</div>
                  </ms-select>
                </div>
              </div>
            </td>
            <td>
              <div>
                <div style="text-align: center">Ngừng theo dõi</div>
                <div class="m-filter-box" style="border: none">
                  <ms-select
                    :value="'Không'"
                    :disable="true"
                    style="background-color: #fff"
                  >
                    <div class="ms-select-item">Không</div>
                    <div class="ms-select-item">Có</div>
                  </ms-select>
                </div>
              </div>
            </td>
          </tr>
        </thead>
        <tbody>
          <tr
            class="tbl-row"
            v-for="(item, index) in data"
            :key="index"
            @click="
              rowClick(item[tableConfig.id], item[tableConfig.code], $event)
            "
            @dblclick="btnEditOnClick"
          >
            <td v-for="(head, index) in tableConfig.header" :key="index">
              {{ item[head.columnName] }}
            </td>
            <td class="text-align-center">
              <input type="checkbox" name="" id="" />
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="m-pagination">
      <div
        class="m-btn-pagi fas fa-angle-double-left"
        @click="btnFirstOnClick()"
      ></div>
      <div
        class="m-btn-pagi fas fa-angle-left"
        @click="btnNextPrevOnClick(-1)"
      ></div>
      <div style="color: #cccccc; font-size: 20px; padding: 0 6px">|</div>
      <nav>Trang</nav>
      <input
        type="text"
        v-model="pageNumber"
        @change="pageNumberOnChange"
        style="width: 24px; margin: 0 6px"
      />
      <nav>trên {{ totalPage }}</nav>
      <div style="color: #cccccc; font-size: 20px; padding: 0 6px">|</div>
      <div
        class="m-btn-pagi fas fa-angle-right"
        @click="btnNextPrevOnClick(1)"
      ></div>
      <div
        class="m-btn-pagi fas fa-angle-double-right"
        @click="btnLastOnClick()"
      ></div>
      <div style="color: #cccccc; font-size: 20px; padding: 0 6px">|</div>
      <div class="m-btn-pagi fas fa-redo-alt" style="font-size: 24px" @click="btnRefreshOnClick"></div>
      <div style="color: #cccccc; font-size: 20px; padding: 0 6px">|</div>
      <ms-select
        :value="pageSize"
        :disable="true"
        :showInBottom="true"
        :width="'20px'"
      >
        <div
          class="ms-select-item"
          v-for="(item, index) in dataPageSizes"
          :key="index"
          :class="{ 'ms-select-active': pageSize == item }"
          @click="selectPageSize(item)"
        >
          {{ item }}
        </div>
      </ms-select>
      <div class="m-space"></div>
      <div class="m-pagi-summary">
        Hiển thị {{ start }} - {{ end }} trên {{ totalRecord }} kết quả
      </div>
    </div>
  </div>
  <div class="m-expand-compare"></div>
</template>

<script>
import ResourceVN from "@/utils/resourceVN";
import { Filter } from "@/utils/constant";
export default {
  name: "MSGird",
  emits: [
    "clickAdd",
    "clickEdit",
    "clickClone",
    "clickDelete",
    "filter",
  ],
  props: ["tableConfig", "data", "filter", "totalPage", "totalRecord"],
  data() {
    return {
      id: null,
      code: null,
      pageSize: 20,
      pageNumber: 1,
      filters: [],
      dataPageSizes: [10, 20, 30, 50, 100],
      filterOption: Filter,
      isShowExpand: new Array(100),
      timeout: null
    };
  },
  watch: {
    /**
     * Khi filter thay đổi gán các giá trị
     * @author: LNTGiang
     * Created date: 15:19 01/04/2022
     */
    data(value) {
      var rows = document.getElementsByClassName("tbl-row");
      Array.from(rows).forEach((element) => {
        element?.classList.remove("m-row-selected");
      });

      this.id = null;
      if (value) {
        this.pageSize = this.filter.pageSize;
        this.pageNumber = this.filter.pageNumber;
      }
    },
  },
  computed: {
    /**
     * Gán STT bản ghi đầu tiên đang được hiển thị
     * @author: LNTGiang
     * Created date: 15:19 01/04/2022
     */
    start: function () {
      let res = 0;
      if (this.totalRecord > 0) {
        res = (this.pageNumber - 1) * this.pageSize + 1;
      }
      return res;
    },
    /**
     * Gán STT bản ghi cuối đang được hiển thị
     * @author: LNTGiang
     * Created date: 15:19 01/04/2022
     */
    end: function () {
      let res = this.pageNumber * this.pageSize;
      if (res > this.totalRecord) {
        res = this.totalRecord;
      }
      return res;
    },
  },

  beforeMount() {
    this.tableConfig.header.forEach((element) => {
      this.filters.push({
        ColumnName: element.filterColumnName,
        Calculation: 100,
        Value: "",
        Display: ""
      });
    });
  },
  methods: {
    /**
    * Khi thay đổi pageNumber trên input
    * @author: LNTGiang
    * Created date: 15:00 15/04/2022
    */
    pageNumberOnChange(){
      if(this.pageNumber <= 0 || this.pageNumber > this.totalPage){
        this.pageNumber = 1;
      }
      this.search();
    },
    /**
    * Xóa option đã chọn của các filter select option
    * @param index - vị trí của filter
    * @author: LNTGiang
    * Created date: 11:49 14/04/2022
    */
    clearSelectOption(index){
      this.filters[index].Value = "";
      this.filters[index].Display = "";
      this.search();
    },
    /**
     * Sự kiện khi click nút Thêm
     * @author: LNTGiang
     * Created date: 09:32 30/03/2022
     */
    btnAddOnClick() {
      this.$emit("clickAdd");
    },

    /**
     * Sự kiện khi click button edit
     * @author: LNTGiang
     * Created date: 11:48 31/03/2022
     */
    btnEditOnClick() {
      if (!this.id) {
        this.$toast.warning(ResourceVN.EditWarn);
        return;
      }
      this.$emit("clickEdit", this.id);
    },
    /**
     * Hiển thị filter option
     * @param index
     * @author: LNTGiang
     * Created date: 09:35 07/04/2022
     */
    expandFilter(index) {
      this.isShowExpand[index] = !this.isShowExpand[index];
    },
    /**
     * Sự kiện khi click button nhân bản
     * @author: LNTGiang
     * Created date: 15:16 31/03/2022
     */
    btnCloneOnClick() {
      if (!this.id) {
        this.$toast.warning(ResourceVN.CloneWarn);
        return;
      }
      this.$emit("clickClone", this.id);
    },

    /**
     * Sự kiện khi click button xóa
     * @author: LNTGiang
     * Created date: 15:17 31/03/2022
     */
    btnDeleteOnClick() {
      if (!this.id) {
        this.$toast.warning(ResourceVN.DeleteWarn);
        return;
      }
      this.$emit("clickDelete", this.id, this.code);
    },

    /**
     * Sự kiện khi click button nạp
     * @author: LNTGiang
     * Created date: 15:09 01/04/2022
     */
    btnRefreshOnClick() {
      this.filters.forEach(element => {
        element.Value = "";
        element.Display = "";
      });
      this.pageNumber = "1";
      this.search();
    },

    /**
     * Sự kiện khi click vào dòng trên table
     * @param id id của dòng dữ liệu
     * @param code mã của dòng dữ liệu
     * @param event
     * @author: LNTGiang
     * Created date: 11:47 31/03/2022
     */
    rowClick(id, code, event) {
      var rows = document.getElementsByClassName("tbl-row");
      Array.from(rows).forEach((element) => {
        if (element?.classList.contains("m-row-selected")) {
          element.classList.remove("m-row-selected");
        }
        if (element?.contains(event.target)) {
          element.classList.add("m-row-selected");
        }
      });
      this.id = id;
      this.code = code;
    },
    /**
     * Chọn PageSize
     * @param trang đã chọn
     * @author: LNTGiang
     * Created date: 11:44 04/04/2022
     */
    selectPageSize(pageSize) {
      this.pageSize = pageSize;
      this.pageNumber = 1;
      this.search();
    },
    /**
     * Click vào button prev hoặc next
     * @param value (prev: -1, next: 1)
     * @author: LNTGiang
     * Created date: 14:46 04/04/2022
     */
    btnNextPrevOnClick(value) {
      let pageNumber = this.pageNumber + value;
      if (pageNumber > 0 && pageNumber <= this.totalPage) {
        this.pageNumber = pageNumber;
        this.search();
      }
    },
    /**
     * Click vào button first
     * @author: LNTGiang
     * Created date: 14:49 04/04/2022
     */
    btnFirstOnClick() {
      if (this.pageNumber == 1) {
        return;
      }
      this.pageNumber = 1;
      this.search();
    },
    /**
     * Click vào button last
     * @author: LNTGiang
     * Created date: 14:50 04/04/2022
     */
    btnLastOnClick() {
      if (this.pageNumber == this.totalPage) {
        return;
      }
      this.pageNumber = this.totalPage;
      this.search();
    },
    /**
     * Thực hiên tìm kiếm phân trang
     * @author: LNTGiang
     * Created date: 14:45 04/04/2022
     */
    search() {
      let newFilter = [];
      this.filters.forEach(element => {
        if(element.Value){
          newFilter.push(element);
        }
      });
      this.$emit("filter", this.pageSize, this.pageNumber, newFilter);
    },
    /**
    * Thay đổi phép toán tìm kiếm
    * @param index vị trí cột
    * @param item phép toán chọn
    * @author: LNTGiang
    * Created date: 15:36 07/04/2022
    */
    filterOptionChange(index, item){
      this.filters[index].Calculation = item.value;
      this.isShowExpand[index] =false;
      this.search();
    },
    /**
    * Thay đổi giá trị tìm kiếm
    * @author: LNTGiang
    * Created date: 15:37 07/04/2022
    */
    inputChange(){
      if(this.timeout){
        clearTimeout(this.timeout)
      }
      this.timeout = setTimeout(() =>{
        this.search()
      }, 500)
    },
    /**
    * Thay đổi giá trị tìm kiếm của loại tìm kiếm select option
    * @param index vị trí cột
    * @param item giá trị chọn
    * @author: LNTGiang
    * Created date: 14:09 08/04/2022
    */
    filterSelectOptionOnClick(index, item){
      this.filters[index].Value = item.Value;
      this.filters[index].Display = item.Name;
      this.filters[index].Calculation = Filter.EQual.value;
      this.search();
    }
  },
};
</script>

<style scoped>
@import url("../../style/base/gird.css");
</style>
