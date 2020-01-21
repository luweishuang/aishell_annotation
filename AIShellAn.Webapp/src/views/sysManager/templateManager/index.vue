<template>
  <Card>
    <p slot="title">
      <Icon type="ios-film-outline"></Icon>标注模板管理
    </p>
    <Button
      type="success"
      style="margin-bottom:10px;"
      size="large"
      @click="addtemplateModal=true"
    >添加模板</Button>
    <Table :columns="templateColumns" :data="templateList">
         <template slot-scope="{ row, index }" slot="action">
        <div style="padding:5px;">
          <Button type="info"  @click="edit(row)" >编辑</Button>
          <Button type="error" style="margin-left:8px;"  @click="deleteTemplate(row)" >删除</Button>
        </div>
      </template>

    </Table>

    <Modal v-model="addtemplateModal" :title="modalTitle" :width="1000" :mask="true" :mask-closable="false">
      <Form :label-width="120">
        <FormItem label="模板名称：">
          <Input autocomplete="off" v-model="template.name"></Input>
        </FormItem>
        <FormItem label="模板类型：">
            <RadioGroup v-model="template.type" type="button">
                    <Radio label="音频"></Radio>
                    <Radio label="图片"></Radio>
                </RadioGroup>
        </FormItem>
        
         <FormItem
          v-for="(item, index) in template.items"
          :key="index"
          :label="'标注项' + index+'：'"
        >
          <Row :gutter="6">
              <Col span="3" >
               <Select placeholder="是否有效" v-model="item.effect">
                   <Option :value="1">有效</Option>
                   <Option :value="0">无效</Option>
               </Select>
            </Col>
             <Col span="3" >
               <Select placeholder="是否必填" v-model="item.necessary">
                   <Option :value="1">必填</Option>
                   <Option :value="0">非必填</Option>
               </Select>
            </Col>
            <Col span="4" >
              <Input type="text" v-model="item.name"  placeholder="名称"></Input>
            </Col>
             
             <Col span="3" >
               <Select placeholder="类型" v-model="item.type">
                   <Option value="单选">单选</Option>
                   <Option value="多选">多选</Option>
                   <Option value="文本框">文本框</Option>
               </Select>
            </Col>
             <Col span="5"  v-if="item.type!='文本框'">
              <Input type="text" v-model="item.value" placeholder="选项内容，英文逗号分隔"></Input>
            </Col>
             <Col span="2"  v-if="item.type!='文本框'">
              <Input type="text" v-model="item.defaultValue" placeholder="默认值"></Input>
            </Col>
            <Col span="2" >
              <Button type="error" @click="handleRemove(index)">删除</Button>
            </Col>
          </Row>
        </FormItem>
        <FormItem>
          <Row>
            <Col span="12">
              <Button type="dashed" long @click="handleAdd" icon="plus-round">添加标注项目</Button>
            </Col>
          </Row>
        </FormItem>
        <FormItem>
             <Button type="info"  @click="saveTemplate" >保存</Button>
        </FormItem>
         
      </Form>
      <div slot="footer"></div>
    </Modal>
  </Card>
</template>
<script>

export default {
  data() {
    return {
        index:0,
      addtemplateModal: false,
      modalTitle:"添加模板",
      templateList: [],
      templateColumns: [
        {
          title: "序号",
          type: "index",
          align: "center"
        },
        {
          title: "模板名称",
          key: "name",
          align: "center"
        },
        {
          title: "模板类型",
          key: "type",
          align: "center"
        },
        {
          title: "操作",
          slot:"action",
          align: "center"
        }
      ],
      template: {
        name: "",
        type:"音频",
        items: []
      },
      
    };
  },
  methods: {
    handleAdd() {
        this.index+=1;
      this.template.items.push({
        value: "",
        index:  this.index,
      });
    },
    handleRemove(index) {
      this.template.items.splice(index,1);
    },
    saveTemplate(){
        let _this=this;
        
        addTemplate(this.template).then(res=>{
            _this.$Message.success(res.data);
            _this.addtemplateModal=false;
            _this.getData();

        }).catch(err=>{
             _this.$Message.error(err.response.data.Message);
        })
    },
    getData(){
        let _this=this;
        getTemplateList().then(res=>{
            _this.templateList=res.data;
           
        })
    },
    edit(row){
        let _this=this;
        _this.modalTitle="编辑模板";
        getTemplate(row.id).then(res=>{
            _this.template=res.data;
        })
        //_this.template.name=
          _this.addtemplateModal=true;
    },
    deleteTemplate(row){
          let _this=this;
        deleteTemplate(row.id).then(res=>{
                _this.$Message.success(res.data);
                   _this.getData();

        })
    }
  },
  mounted(){
      this.getData();
  }
};
</script>
