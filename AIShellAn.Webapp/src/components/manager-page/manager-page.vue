<!-- 通用的数据管理列表页面模板-->
<template>
  <div>
    <!-- 条件筛选区域 -->
    <Row></Row>
    <!-- 操作按钮区域 -->
    <Row>
      <Button type="primary" icon="md-add" @click="handleAdd">新增</Button>
    </Row>
    <!-- 数据列表区域 -->
    <Card class="margin-top-10">
      <Row>
        <Table :data="data" :columns="columns" :loading="tableLoading" no-data-text="暂无数据" border>
             <template slot-scope="{row}" slot="content">
               123
            </template>
        </Table>
      </Row>
      <!-- 分页组件区域 -->
      <Row class="margin-top-10">
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
export default {
  props: {
    data: {
      type: Array
    },
    columns: {
        type:Array
    },
    page: {}
  },
  data() {
    return {
      tableLoading: false
    };
  },
  methods: {
    //切换页数
    changePage() {},
    //切换每页显示数量
    changePageSize() {},
    handleAdd() {}
  }
};
</script>