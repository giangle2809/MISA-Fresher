<template>
  <div class="m-dialog" :style="[isShow ? 'display:flex' : 'display:none']">
    <div class="m-material-detail">
      <div class="m-detail-header">
        <div>{{dataStatusText}} nguyên vật liệu</div>
        <div class="m-space"></div>
        <div class="m-detail-header-item">
          <i class="fas fa-sort" style="transform: rotate(45deg)"></i>
        </div>
        <div class="m-detail-header-item" @click="closeDialog">
          <i class="fas fa-times"></i>
        </div>
      </div>
      <div class="m-detail-body">
        <div class="m-row">
          <div class="m-w-45">
            <div class="m-row m-mb-6">
              <div class="m-w-40">Tên <span style="color: red">(*)</span></div>
              <div class="m-w-60">
                <input
                  class="m-input m-input-border pl-10"
                  type="text"
                  v-model="materialName"
                  ref="MaterialName"
                  tabindex="1"
                  @change="changeCodeByName"
                  @blur="valueOnChange('MaterialName')"
                  maxlength="255"
                />
              </div>
            </div>
            <div class="m-row m-mb-6">
              <div class="m-w-40">ĐVT <span style="color: red">(*)</span></div>
              <div class="m-w-60">
                <ms-select
                  :value="material?.UnitName"
                  :disable="true"
                  :tabindex="2"
                >
                  <div
                    class="ms-select-item"
                    v-for="(item, index) in unit"
                    :key="index"
                    :class="{
                      'ms-select-active': material?.UnitId == item.DataId,
                    }"
                    @click="selectUnit(item)"
                  >
                    {{ item.DataName }}
                  </div>
                </ms-select>
              </div>
            </div>
            <div class="m-row m-mb-6">
              <div class="m-w-40">Hạn sử dụng</div>
              <div class="m-w-60">
                <date-picker
                  class="m-input"
                  v-model:value="expiry"
                  placeholder="DD/MM/YYYY"
                  format="DD/MM/YYYY"
                  lang="vi"
                  tabindex="3"
                  ref="expiry"
                >
                </date-picker>
              </div>
            </div>
          </div>
          <div class="m-w-10"></div>
          <div class="m-w-45">
            <div class="m-row m-mb-6">
              <div class="m-w-40">Mã <span style="color: red">(*)</span></div>
              <div class="m-w-60">
                <input
                  class="m-input m-input-border pl-10"
                  type="text"
                  v-model="materialCode"
                  ref="MaterialCode"
                  tabindex="4"
                  maxlength="20"
                  @blur="valueOnChange('MaterialCode')"
                />
              </div>
            </div>
            <div class="m-row m-mb-6">
              <div class="m-w-40">Kho ngầm định</div>
              <div class="m-w-60">
                <ms-select
                  :value="material?.StockName"
                  :disable="true"
                  :tabindex="5"
                >
                  <div
                    class="ms-select-item"
                    v-for="(item, index) in stock"
                    :key="index"
                    :class="{
                      'ms-select-active': material?.StockId == item.DataId,
                    }"
                    @click="selectStock(item)"
                  >
                    {{ item.DataName }}
                  </div>
                </ms-select>
              </div>
            </div>
            <div class="m-row m-mb-6">
              <div class="m-w-40">SL tồn tối thiểu</div>
              <div class="m-w-60">
                <input
                  class="m-input m-input-border pl-10"
                  type="text"
                  v-model="minimumStock"
                  ref="minimumStock"
                  tabindex="6"
                />
              </div>
            </div>
          </div>
        </div>
        <div class="m-row m-mb-6">
          <div class="m-w-18">Mô tả</div>
          <div class="m-w-82">
            <textarea
              class="m-input m-input-border"
              type="text"
              v-model="description"
              style="width: 100%; height: 80px"
              ref="description"
              tabindex="7"
              maxlength="255"
            />
          </div>
        </div>
        <div class="m-row m-mb-6 m-tab">
          <div class="m-tab-item">Đơn vị chuyển đổi</div>
        </div>
        <div class="m-detail-gird">
          <unit-conversion-list
            :materialId="materialIdSelected"
            :unitName="material?.UnitName"
            :status="dataStatus"
            :isClear="!isShow"
            @dataChange="unitConversionChange"
          ></unit-conversion-list>
        </div>

        <div class="m-detail-footer">
          <ms-button :classBtn="'m-btn-secondary'" :icon="'mi mi-16 mi-help'"
            >Giúp</ms-button
          >
          <div class="m-space"></div>
          <ms-button
            :classBtn="'m-btn-secondary'"
            :icon="'mi mi-16 mi-save'"
            @click="save(false)"
            >Cất</ms-button
          >
          <ms-button
            :classBtn="'m-btn-secondary'"
            :icon="'mi mi-16 mi-save-and-add'"
            style="margin: 0 10px"
            @click="save(true)"
            >Cất & Thêm</ms-button
          >
          <ms-button
            :classBtn="'m-btn-secondary'"
            :icon="'mi mi-16 mi-cancel'"
            @click="closeDialog"
            >Hủy bỏ</ms-button
          >
        </div>
      </div>
    </div>
    <ms-loading :isShow="isLoading"></ms-loading>
  </div>
</template>

<script>
import MaterialRepository from "@/repository/materialRepository";
import UnitConversionList from "../unitConversion/UnitConversionList.vue";
import { DataStatus } from "@/utils/constant";
import MasterDataRepository from "@/repository/masterDataRespository";
import ResourceVN from "@/utils/resourceVN";

export default {
  name: "MaterialDetail",
  components: { UnitConversionList },
  emits: ["closeDialog", "refresh"],
  props: ["materialIdSelected", "isShow", "dataStatus"],
  data() {
    return {
      isLoading: false,
      material: {},
      stock: [],
      unit: [],
      unitConversions: [],
      dataStatusText: '',
      oldPrefix: ''
    };
  },
  watch: {
    /**
     * Theo dõi khi đóng mở popup
     * @param value gia trị của isShow
     * @author: LNTGiang
     * Created date: 14:12 31/03/2022
     */
    isShow: function (value) {
      if (value) {
        this.material = {};
        this.$nextTick(() => this.$refs["MaterialName"].focus());
        if (
          this.dataStatus === DataStatus.Edit ||
          this.dataStatus === DataStatus.Clone
        ) {
          this.loadMaterial();
        }
        if (
          this.dataStatus === DataStatus.Add ||
          this.dataStatus === DataStatus.Clone
        ) {
          this.loadNewMaterialCode();
        }
        if(this.dataStatus === DataStatus.Edit){
          this.dataStatusText = "Sửa";
        }
        else if(this.dataStatus === DataStatus.Add){
          this.dataStatusText = "Thêm";
        }
        else{
          this.dataStatusText = "Nhân bản";
        }
      }
    },
  },
  computed: {
    materialName: {
      get() {
        return this.material?.MaterialName;
      },
      set(newVal) {
        if (!this.material) {
          this.material = {};
        }
        this.material.MaterialName = newVal;
      },
    },
    materialCode: {
      get() {
        return this.material?.MaterialCode;
      },
      set(newVal) {
        if (!this.material) {
          this.material = {};
        }
        this.material.MaterialCode = newVal;
      },
    },
    unitId: {
      get() {
        return this.material?.UnitId;
      },
      set(newVal) {
        if (this.material) {
          this.material = {};
        }
        this.material.UnitId = newVal;
      },
    },
    stockId: {
      get() {
        return this.material?.StockId;
      },
      set(newVal) {
        if (!this.material) {
          this.material = {};
        }
        this.material.StockId = newVal;
      },
    },
    expiry: {
      get() {
        if (this.material && this.material.Expiry) {
          return new Date(this.material.Expiry);
        }
        return null;
      },
      set(newVal) {
        if (!this.material) {
          this.material = {};
        }
        this.material.Expiry = newVal;
      },
    },
    minimumStock: {
      get() {
        // let formatNumber = new Intl.NumberFormat('de-DE', {minimumFractionDigits: 2})
        let formatNumber = new Intl.NumberFormat('de-DE')
        if(this.material?.MinimumStock){
          return formatNumber.format(this.material?.MinimumStock);
        }
        return "";
      },
      set(newVal) {
        newVal = newVal.replaceAll('.', '');
        if (!this.material) {
          this.material = {};
        }
        this.material.MinimumStock = parseInt(newVal);
      },
    },
    description: {
      get() {
        return this.material?.Description;
      },
      set(newVal) {
        if (!this.material) {
          this.material = {};
        }
        this.material.Description = newVal;
      },
    },
  },
  created() {
    this.loadUnit();
    this.loadStock();
  },
  methods: {
    /**
    * Khi blur khỏi các trường required
    * @author: LNTGiang
    * Created date: 11:28 14/04/2022
    */
    valueOnChange(name){
      if(this.material[name]){
        this.$refs[name].classList.remove('m-input-error');
      }
      else{
        this.$refs[name].classList.add('m-input-error');
      }
    },
    /**
     * Đóng Dialog
     * @author: LNTGiang
     * Created date: 10:45 30/03/2022
     */
    closeDialog() {
      this.$emit("closeDialog");
    },
    /**
     * Chọn option đơn vị tính
     * @author: LNTGiang
     * Created date: 11:41 04/04/2022
     */
    selectUnit(unit) {
      this.material.UnitId = unit.DataId;
      this.material.UnitName = unit.DataName;
    },
    /**
     * Chọn option kho ngầm định
     * @author: LNTGiang
     * Created date: 11:41 04/04/2022
     */
    selectStock(stock) {
      this.material.StockId = stock.DataId;
      this.material.StockName = stock.DataName;
    },
    /**
     * Validate data
     * @return kết quả validate true - đúng, false - sai
     * @author: LNTGiang
     * Created date: 13:54 04/04/2022
     */
    validate() {
      if (!this.material.MaterialName) {
        this.$toast.error(ResourceVN.MaterialNameEmpty);
        this.$refs['MaterialName'].classList.add('m-input-error')
        return false;
      }
      if (!this.material.MaterialCode) {
        this.$toast.error(ResourceVN.MaterialCodeEmpty);
        this.$refs['MaterialCode'].classList.add('m-input-error')
        return false;
      }
      if (!this.material.UnitId) {
        this.$toast.error(ResourceVN.UnitEmpty);
        return false;
      }
      return true;
    },
    /**
     * Khi dữ liệu của đơn vị chuyển đổi thay đổi thì gán lại
     * @param unitConversions các dữ liệu của đơn vị chuyển đổi
     * @author: LNTGiang
     * Created date: 15:05 06/04/2022
     */
    unitConversionChange(unitConversions) {
      this.unitConversions = unitConversions;
    },
    /**
     * Load thông tin NVL
     * @author: LNTGiang
     * Created date: 14:18 31/03/2022
     */
    async loadMaterial() {
      this.isLoading = true;
      try {
        let res = await MaterialRepository.getById(this.materialIdSelected);
        if (res) {

          this.material = res?.data;
          if(this.material?.MaterialName){
            this.oldPrefix = this.getPrefix(this.material.MaterialName);
          }

          if (this.material && this.material?.UnitId) {
            this.unit.forEach((item) => {
              if (item?.DataId == this.material?.UnitId) {
                this.material.UnitName = item?.DataName;
              }
            });
            this.stock.forEach((item) => {
              if (item?.DataId == this.material?.StockId) {
                this.material.StockName = item?.DataName;
              }
            });
          }
        }
      } catch (ex) {
        console.log(ex);
      }
      this.isLoading = false;
    },
    /**
    * Mô tả code
    * @param
    * @return
    * @author: LNTGiang
    * Created date: 16:23 14/04/2022
    */
    getPrefix(str){
      let arr = str.split(' ');
      let prefix = '';
      arr.forEach(element => {
        prefix += element[0];
      });
      return prefix.toUpperCase();
    },
    /**
    * Thay đổi tiền tố mã NVL khi tên thay đổi
    * @author: LNTGiang
    * Created date: 11:28 14/04/2022
    */
    changeCodeByName(){
      if(this.material?.MaterialName){
        let prefix = this.getPrefix(this.material.MaterialName);

        if(this.oldPrefix){
          this.material.MaterialCode = this.material?.MaterialCode.replace(this.oldPrefix, prefix);
        }
        else{
          this.material.MaterialCode = prefix + this.material?.MaterialCode;
        }
        this.oldPrefix = prefix;
      }
    },
    /**
     * Lấy danh sách đơn vị tính
     * @return Danh sách đơn vị tính
     * @author: LNTGiang
     * Created date: 11:04 04/04/2022
     */
    async loadUnit() {
      try {
        let res = await MasterDataRepository.getUnit();
        if (res) {
          this.unit = res.data;
        }
      } catch (ex) {
        console.log(ex);
      }
    },
    /**
     * Lấy danh sách kho ngầm định
     * @return
     * @author: LNTGiang
     * Created date: 11:04 04/04/2022
     */
    async loadStock() {
      try {
        let res = await MasterDataRepository.getStock();
        if (res) {
          this.stock = res.data;
        }
      } catch (ex) {
        console.log(ex);
      }
    },
    /**
     * Thực hiện lưu dữ liệu
     * @author: LNTGiang
     * @param isSaveAndAdd trạng thái lưu và thêm mới
     * Created date: 11:52 04/04/2022
     */
    async save(isSaveAndAdd) {
      this.isLoading = true;
      if (this.validate()) {
        try {
          if (
            this.dataStatus === DataStatus.Add ||
            this.dataStatus === DataStatus.Clone
          ) {
            let res = await MaterialRepository.insert(
              this.material,
              this.unitConversions.filter(x => x.Mode != 0)
            );
            if (res) {
              this.$toast.success(ResourceVN.MaterialInsertSuccess);
            }
          } else if (this.dataStatus === DataStatus.Edit) {
            let res = await MaterialRepository.update(
              this.materialIdSelected,
              this.material,
              this.unitConversions.filter(x => x.Mode != 0)
            );
            if (res) {
              this.$toast.success(ResourceVN.MaterialUpdateSuccess);
            }
          }
          if (isSaveAndAdd) {
            this.material = {};
          } else {
            this.$emit("refresh");
            this.closeDialog();
          }
        } catch (ex) {
          if (ex) {
            this.$toast.error(ex?.response?.data?.UserMsg);
          }
        }
      }
      this.isLoading = false;
    },
    /**
     * Lấy mã nguyên vật liệu mới
     * @author: LNTGiang
     * Created date: 16:37 04/04/2022
     */
    async loadNewMaterialCode() {
      try {
        let res = await MaterialRepository.getNewMaterialCode();
        if (res) {
          this.material.MaterialCode = res.data;
          this.oldPrefix = "";
        }
      } catch (ex) {
        console.log(ex);
      }
    },
  },
};
</script>

<style scoped>
@import url("../../style/page/material-detail.css");
</style>
