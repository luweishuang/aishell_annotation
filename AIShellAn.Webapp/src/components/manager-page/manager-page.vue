<!-- 通用的数据管理列表页面模板-->
<template>
  <div>
     <Card >
    <!-- 条件筛选区域 -->
    <Row>
      <filterbox ref="filter" :filterList="filterList" @search="search"></filterbox>
    </Row>
    <!-- 操作按钮区域 -->
    <Row class="margin-top-10" type="flex" justify="end" >
       <Col span="24" >
        <ButtonGroup>
          <Button type="primary"  icon="md-add" @click="handleAdd" v-if="this.$listeners['add']">新增</Button>
          <Button type="error"  icon="md-trash" @click="handleAdd" v-if="this.$listeners['bulk-delete']">批量删除</Button>
        </ButtonGroup>
       </Col>
    </Row>
    <!-- 数据列表区域 -->
   
      <Row class="margin-top-10">
        <Table :data="data" :columns="columns" :loading="loading" no-data-text="暂无数据" border>
          <template v-for="(_, name) in $scopedSlots" :slot="name" slot-scope="slotData">
            <slot :name="name" v-bind="slotData" />
          </template>
        </Table>
      </Row>
      <!-- 分页组件区域 -->
      <Row class="margin-top-10"  type="flex" justify="center" >
        <Page
          :total="page.total"
          :page-size="page.size"
          :current="page.current"
          show-elevator
          show-total
          @on-change="changePage"
          @on-page-size-change="changePageSize"
        />
      </Row>
    </Card>
  </div>
</template>
<script>
import filterbox from "./filter-box"
export default {
   components: {
   filterbox
  },
  props: {
    loading: {
      type: Boolean
    },
    data: {
      type: Array
    },
    columns: {
      type: Array
    },
    filterList:{
      type:Array
    },
    page: {}
  },
  data() {
    return {};
  },
  methods: {

    //切换页数
    changePage() {},
    //切换每页显示数量
    changePageSize() {},

    search(){
       this.$emit("search");
    },
    getFilter(){
      return this.$refs.filter.getFilter();
    },
    handleAdd() {
      this.$emit("add");
    }
  },
  mounted(){
    
  }
};
</script>

