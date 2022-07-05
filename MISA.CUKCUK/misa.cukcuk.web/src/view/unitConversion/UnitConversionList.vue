<template>
  <div class="m-conversion-table" style="margin-bottom: 10px">
    <table>
      <thead>
        <tr>
          <th>STT</th>
          <th>Đơn vị chuyển đổi</th>
          <th>Tỷ lệ chuyển đổi</th>
          <th>Phép tính</th>
          <th>Mô tả</th>
        </tr>
      </thead>
      <tbody>
        <tr  class="unitconversion-row" v-for="(item, index) in data" :key="index" @click="rowClick(index, item, $event)">
          <td class="m-row-index">{{ index + 1 }}</td>
          <td><ms-dropdown :options="unit" :selectedValue="item.UnitId" @selected="(value) => unitConvertOptionOnChange(index, 'UnitId', value)"></ms-dropdown></td>
          <td><input class="m-input-table" style="text-align: right" type="text" v-model="item.ConversionRate" @keyup="changeMode(index)"></td>
          <td><ms-dropdown :options="calcu" :selectedValue="item.Calculation" @selected="(value) => unitConvertOptionOnChange(index, 'Calculation', value)"></ms-dropdown></td>
          <td><div class="m-input-table" >{{`1 ${item.UnitName ?? ""} = ${item.ConversionRate ?? ""} ${item.CalculationName ?? ""} ${unitName ?? ""}`}}</div></td>
        </tr>
      </tbody>
    </table>
  </div>
  <div class="m-row" style="margin-bottom: 20px">
    <ms-button
      style="margin-right: 10px"
      :classBtn="'m-btn-secondary'"
      :icon="'mi mi-16 mi-document-add'"
      @click="addRowOnClick"
      >Thêm dòng</ms-button
    >
    <ms-button
      :classBtn="'m-btn-secondary'"
      :isDisable="!isDelete"
      :icon="'mi mi-16 mi-document-delete'"
      @click="btnDeleteOnClick"
      >Xóa dòng</ms-button
    >
  </div>
  <ms-message-box
    :isShow="isShowMessageBox"
    :message='contentMessageBox'
    @close='closeMessageBox'
    @ok='messageBoxOkOnClick'
  ></ms-message-box>
</template>

<script>
import UnitConversionRepository from "@/repository/unitConversionReposition";
import { Calculation, DataStatus, Mode } from '@/utils/constant';
import MasterDataRepository from '@/repository/masterDataRespository';
import ResourceVN from '@/utils/resourceVN';
export default {
  name: "UnitConversionList",
  emits: ['dataChange'],
  props: ["materialId","unitName", "status", "isClear"],
  data() {
    return {
      data: [],
      unit: [],
      calcu: [
        {
          Name: '+',
          Value: Calculation.Add
        },
        {
          Name: '-',
          Value: Calculation.Sub
        },
        {
          Name: '*',
          Value: Calculation.Mul
        },
        {
          Name: '/',
          Value: Calculation.Div
        }
      ],
      isDelete: false,
      itemSelected: null,
      indexSelected: -1,
      dataDeleted: [],
      isShowMessageBox: false,
      contentMessageBox: ''
    };
  },
  created(){
    this.loadUnit();
  },
  watch: {
    /**
     * Khi materialId thay đổi load lại data
     * @author: LNTGiang
     * Created date: 13:44 01/04/2022
     */
    materialId(value) {
      this.data = [];
      this.itemSelected = null;
      this.isDelete = false;
      if (value) {
        this.loadData();
      }
    },
    /**
    * Thực hiện xóa dữ liệu sau khi đóng popup
    * @author: LNTGiang
    * Created date: 14:07 15/04/2022
    */
    isClear(value){
      if(value){
        this.data = [];
        this.dataDeleted = [];
      }
    }
  },
  methods: {
    /**
    * Show popup message
    * @param message thông tin cần hiển thị
    * @author: LNTGiang
    * Created date: 14:42 01/04/2022
    */
    showMessageBox(message){
      this.isShowMessageBox = true;
      this.contentMessageBox = message;
    },

    /**
    * Đóng popup message
    * @author: LNTGiang
    * Created date: 14:43 01/04/2022
    */
    closeMessageBox(){
      this.isShowMessageBox = false;
    },
    /**
     * Load dữ liệu
     * @author: LNTGiang
     * Created date: 13:43 01/04/2022
     */
    async loadData() {
      try {
        let res = await UnitConversionRepository.getByMaterialId(
          this.materialId
        );
        if (res) {
          this.data = res.data;
          this.dataChange();
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Lấy danh sách đơn vị tính
     * @return
     * @author: LNTGiang
     * Created date: 14:04 05/04/2022
     */
    async loadUnit() {
      try {
        let res = await MasterDataRepository.getUnit();
        if (res) {
          res.data.forEach(element => {
            this.unit.push({
              Value: element.DataId,
              Name: element.DataName
            })
          });
        }
      } catch (ex) {
        console.log(ex);
      }
    },
    /**
    * Thêm dòng mới
    * @author: LNTGiang
    * Created date: 15:15 04/04/2022
    */
    addRowOnClick(){
        this.data.push({})
    },
    /**
    * Khi click vào 1 dòng
    * @author: LNTGiang
    * Created date: 08:42 07/04/2022
    */
    rowClick(index, item, event){
      var rows = document.getElementsByClassName("unitconversion-row");
      Array.from(rows).forEach((element) => {
        if (element?.classList.contains("m-row-selected")) {
          element.classList.remove("m-row-selected");
        }
        if (element?.contains(event.target)) {
          element.classList.add("m-row-selected");
        }
      });
      this.indexSelected = index;
      this.itemSelected = item;
      this.isDelete = true;
    },
    /**
    * Hiện Messagebox hỏi có muốn xóa đơn vị chuyển đổi không
    * @author: LNTGiang
    * Created date: 14:47 15/04/2022
    */
    btnDeleteOnClick(){
      this.showMessageBox(ResourceVN.DeleteUnitConversion);
    },
    /**
    * Xóa dòng đã chọn
    * @author: LNTGiang
    * Created date: 08:50 07/04/2022
    */
    messageBoxOkOnClick(){
      if(this.isDelete){
        this.data.splice(this.indexSelected, 1);
        this.itemSelected.Mode = Mode.Delete;
        this.dataDeleted.push(this.itemSelected);
        var rows = document.getElementsByClassName("unitconversion-row");
        Array.from(rows).forEach((element) => {
          if (element?.classList.contains("m-row-selected")) {
            element.classList.remove("m-row-selected");
          }
          if (element?.contains(event.target)) {
            element.classList.add("m-row-selected");
          }
        });
        this.indexSelected = -1;
        this.itemSelected = null;
        this.isDelete = false;
      }
      this.closeMessageBox();

    },
    /**
    * Sự kiện khi thay đổi option
    * @author: LNTGiang
    * Created date: 17:08 05/04/2022
    */
    unitConvertOptionOnChange(index, columnName, value){
      this.data[index][columnName] = value.Value;
      let name = columnName.replace('Id', '');
      this.data[index][`${name}Name`] = value.Name;
      this.changeMode(index);
    },
    /**
    * Khi data thay đổi update lại mode
    * @author: LNTGiang
    * Created date: 21:10 06/04/2022
    */
    changeMode(index){
      if(this.data[index].UnitConversionId){
        this.data[index].Mode = Mode.Edit;
      }
      else{
        this.data[index].Mode = Mode.Add;
      }
      this.dataChange();
    },
    /**
    * Gửi dữ liệu lên khi data thay đổi
    * @author: LNTGiang
    * Created date: 15:01 06/04/2022
    */
    dataChange(){
      if(this.status == DataStatus.Edit){
        this.$emit('dataChange', this.data.concat(this.dataDeleted));
      }
      else{
        this.$emit('dataChange', this.data);
      }
    }
  },
};
</script>

<style scoped>
@import url("../../style/page/unit-conversion-list.css");
</style>
