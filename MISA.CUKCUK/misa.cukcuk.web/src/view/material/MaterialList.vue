<template>
  <div class="m-material-header">
    <div class="m-title">Nguyên vật liệu</div>
    <div class="m-space"></div>
    <ms-button :classBtn="'m-btn-secondary'" :icon="'mi mi-16 mi-speaker'"
      >Phản hồi</ms-button
    >
  </div>
  <div class="m-material-body">
    <ms-gird
     :tableConfig="tableConfig"
     :data="materials"
     :filter="filter"
     :totalPage="totalPage"
     :totalRecord="totalRecord"
     @clickAdd="btnAddOnClick"
     @clickEdit="btnEditOnClick"
     @clickClone="btnCloneOnClick"
     @clickDelete="btnDeleteOnClick"
     @filter="filterData"
     ></ms-gird>
  </div>
  <material-detail 
    :isShow="isShowDialog"
    :materialIdSelected="materialIdSelected"
    :dataStatus="dataStatus"
    @closeDialog="closeDialog"
    @refresh="loadData()"
    />
  <ms-message-box
    :isShow="isShowMessageBox"
    :message='contentMessageBox'
    @close='closeMessageBox'
    @ok='messageBoxOkOnClick'
  ></ms-message-box>
  <ms-loading :isShow='isLoading'></ms-loading>
</template>

<script>
import MaterialDetail from "./MaterialDetail.vue";
import MaterialRepository from '@/repository/materialRepository'
import {DataStatus} from '@/utils/constant'
import Format from '@/utils/format';
import ResourceVN from '@/utils/resourceVN';
import MasterDataRepository from '@/repository/masterDataRespository';

export default {
  components: { MaterialDetail },
  name: "MaterialList",
  data() {
    return {
      isLoading: false,
      isShowDialog: false,
      tableConfig: {
        id: 'MaterialId',
        code: 'MaterialCode',
        header: [
          {
            columnDisplayName: 'Mã nguyên vật liệu',
            columnName: 'MaterialCode',
            filterColumnName: 'MaterialCode',
            compare: 'text'
          },
          {
            columnDisplayName: 'Tên nguyên vật liệu',
            columnName: 'MaterialName',
            filterColumnName: 'MaterialName',
            compare: 'text'
          },
          {
            columnDisplayName: 'Số lượng tồn kho tối thiểu',
            columnName: 'MinimumStock',
            filterColumnName: 'MinimumStock',
            compare: 'number'
          },
          {
            columnDisplayName: 'Đơn vị tính',
            columnName: 'UnitName',
            filterColumnName: 'UnitId',
            compare: 'option',
            data: []
          },
          {
            columnDisplayName: 'Kho ngầm định',
            columnName: 'StockName',
            filterColumnName: 'StockId',
            compare: 'option',
            data: []
          },
          {
            columnDisplayName: 'Ghi chú',
            columnName: 'Description',
            filterColumnName: 'Description',
            compare: 'text'
          },
        ]
      },
      materials: [],
      filter: {
        pageSize: 20,
        pageNumber: 1,
        filters: ''
      },
      totalPage: 0,
      totalRecord: 0,
      materialIdSelected: null,
      dataStatus: DataStatus.Add,
      isShowMessageBox: false,
      contentMessageBox: '',
    };
  },
  created(){
    this.loadData();
    this.loadUnit();
    this.loadStock();
  },
  methods: {
    /**
     * Show Dialog chi tiết nhân viên
     * @author: LNTGiang
     * Created date: 09:35 30/03/2022
     */
    showDialog() {
      this.isShowDialog = true;
    },
    /**
     * Đóng Dialog chi tiết nhân viên
     * @author: LNTGiang
     * Created date: 09:35 30/03/2022
     */
    closeDialog() {
      this.isShowDialog = false;
      this.materialIdSelected = null;
    },

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
     * Nhận sự kiện khi ấn thêm mới bên gird
     * @author: LNTGiang
     * Created date: 09:34 30/03/2022
     */
    btnAddOnClick() {
      this.dataStatus = DataStatus.Add;
      this.materialIdSelected = null;
      this.showDialog()
    },

    /**
    * Nhận sự kiện khi ấn sửa mới bên gird
    * @param materialId id của NVL
    * @author: LNTGiang
    * Created date: 11:50 31/03/2022
    */
    btnEditOnClick(materialId){
      this.dataStatus = DataStatus.Edit;
      this.materialIdSelected = materialId;
      this.showDialog();
    },

    /**
    * Sự kiện khi ấn nhân bản
    * @param materialId id của NVL
    * @author: LNTGiang
    * Created date: 15:18 31/03/2022
    */
    btnCloneOnClick(materialId){
      this.dataStatus = DataStatus.Clone;
      this.materialIdSelected = materialId;
      this.showDialog();
    },

    /**
    * Sự kiện khi ấn đồng ý messagevox
    * @author: LNTGiang
    * Created date: 15:02 01/04/2022
    */
    messageBoxOkOnClick(){
      try {
        if(this.dataStatus === DataStatus.Delete){
          this.deleteData();
        }
      } catch (error) {
        console.log(error);
      }
      finally{
        this.closeMessageBox()
      }
    },

    /**
    * Sự kiện khi ấn xóa
    * @param materialId id của NVL
    * @author: LNTGiang
    * Created date: 15:34 31/03/2022
    */
    btnDeleteOnClick(materialId, materialCode){
      this.dataStatus = DataStatus.Delete;
      this.materialIdSelected = materialId;
      this.showMessageBox(Format.stringFormat(ResourceVN.messageDelete, [materialCode]));
    },

    /**
    * Sự kiện khi tìm kiếm hoặc phân trang
    * @param pageSize số bản ghi trên 1 trang
    * @param pageNumber trang thứ mấy
    * @param filterStr nội dung tìm kiếm
    * @return
    * @author: LNTGiang
    * Created date: 14:14 04/04/2022
    */
    filterData(pageSize, pageNumber, filters){
      this.filter.pageSize = pageSize;
      this.filter.pageNumber = pageNumber;
      this.filter.filters = filters;
      this.loadData();
    },

    /**
    * load dữ liệu
    * @author: LNTGiang
    * Created date: 11:03 31/03/2022
    */
    async loadData(){
      this.isLoading = true;
      try{
        let res = await MaterialRepository.filter(this.filter.pageSize, this.filter.pageNumber, this.filter.filters);
        if(res?.data){
          this.materials = res.data.Data;
          this.totalPage = res.data.TotalPage;
          this.totalRecord = res.data.TotalRecord;

          this.materials.forEach(element => {
            let formatNumber = new Intl.NumberFormat('de-DE');
            console.log(element.MinimumStock);
            element.MinimumStock = formatNumber.format(element.MinimumStock);
          })
        }
      }catch(ex){
        console.log(ex);
      }
      this.isLoading = false;
    },

    /**
    * Xóa dữ liệu
    * @author: LNTGiang
    * Created date: 15:02 01/04/2022
    */
    async deleteData(){
      this.isLoading = true;
      try {
        let res = await MaterialRepository.delete([this.materialIdSelected]);
        if(res){
          this.$toast.success(ResourceVN.MaterialDeleteSuccess)
          this.loadData();
        }
      } catch (error) {
        console.log(error);
      }
      this.isLoading = false;
    },
    /**
     * Lấy danh sách đơn vị tính
     * @return
     * @author: LNTGiang
     * Created date: 14:04 05/04/2022
     */
    async loadUnit() {
      this.isLoading = true;
      try {
        let res = await MasterDataRepository.getUnit();
        if (res) {
          res.data.forEach(element => {
            this.tableConfig.header[3].data.push({
              Value: element.DataId,
              Name: element.DataName
            })
          });
        }
      } catch (ex) {
        console.log(ex);
      }
      this.isLoading = false;
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
          res.data.forEach(element => {
            this.tableConfig.header[4].data.push({
              Value: element.DataId,
              Name: element.DataName
            })
          });

        }
      } catch (ex) {
        console.log(ex);
      }
    },
  },
};
</script>

<style>
@import url("../../style/page/material-list.css");
</style>
