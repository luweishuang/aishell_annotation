<!-- 通用筛选内容组件 -->
<template>
  <div>
    <Form ref="filterForm" :label-width="120">
      <Row :gutter="16"  >
        <Col span="6" v-for="(item,index) in filterListTemp" :key="index">
          <FormItem :prop="item.name" :label="item.name+'：'">
            <template v-if="item.type=='input'">
              <Input type="text" v-model="filter[item.fieldName]" placeholder="请填写" clearable></Input>
            </template>
            <template v-if="item.type=='select'">
              <Select v-model="filter[item.fieldName]" placeholder="请选择" clearable>
                <Option
                  v-for="(selectItem,selectIndex) in item.props.selectData"
                  :value="selectItem.value"
                  :key="selectIndex"
                >{{ selectItem.label }}</Option>
              </Select>
            </template>
             <template v-if="item.type=='date'">
                    <DatePicker type="date" v-model="filter[item.fieldName]" placeholder="请选择" clearable></DatePicker>
             </template>
               <template v-if="item.type=='daterange'">
                    <DatePicker type="daterange" v-model="filter[item.fieldName]" placeholder="请选择"  placement="bottom-end" style="width:100%;" clearable></DatePicker>
             </template>
          </FormItem>
        </Col>
        <Col span="6"  :push="(4-(filterListTemp.length%4))*6-6" class="ivu-text-right">
          <Button type="primary" @click="handleSearch">查询</Button>
          <Button class="ivu-ml-8" @click="resetForm">重置</Button>
          <template v-if="!spread">
               <a  class="ivu-ml-8" @click="spread=!spread">展开<Icon type="ios-arrow-down" /></a>
          </template>
          <template v-else>
               <a  class="ivu-ml-8" @click="spread=!spread">收缩<Icon type="ios-arrow-up" /></a>
          </template>
        </Col>
      </Row>
    </Form>
  </div>
</template>
<script>
export default {
  props: ['filterList'],
  data() {
    return {
      filterListTemp:this.filterList.slice(0,2),
      spread:false,
      filter:{}
    };
  },
  watch:{
      spread(value){
          if(value){
              this.filterListTemp=this.filterList;
          }else{
              this.filterListTemp=this.filterList.slice(0,2);
          }
      }
  },
  methods:{
      resetForm(){
          //重置表单信息
          this.filter={};
          //this.$refs.filterForm.resetFields();
      },
      getFilter(){
          return this.filter;
      },
      handleSearch(){
           this.$emit("search");
      }
  }
};
</script>

