<template>
  <div>
    <Row>
      <Col>
        <Upload
          ref="upload"
          type="drag"
          :action="action"
          :headers="headers"
          :before-upload="handleUpload"
          :on-error="onError"
          :format="['xlsx']"
          :max-size="20480"
          :on-exceeded-size="handleMaxSize"
          :on-format-error="handleFormatError"
          :show-upload-list="true"
          :on-success="handleSuccess"
        >
          <div style="padding: 20px 0">
            <Icon type="ios-cloud-upload" size="52" style="color: #3399ff"></Icon>
            <p>拖动文件到此处，或者点击此处选择文件</p>
          </div>
          <div v-if="files.length>0">已经选择文件： {{ files[0].name }} 【{{ parseInt(files[0].size/1024)}}kb】 </div>
        </Upload>
      </Col>
      <Col></Col>
    </Row>

    <div style="width:80px;margin:0 auto;" @click="startUpload">
      <Button :loading="loadingStatus" type="primary">开始上传</Button>
    </div>
  </div>
</template>
<script>
export default {
  data() {
    return {
      action: window.ajaxUrl + "/api/File/UploadUserTemplate/",
      headers: {
        Authorization: `Bearer ${this.$store.state.user.token}`,
        Accept: "application/json"
      },
      files: [],
      loadingStatus: false
    };
  },
  methods: {
    handleUpload(file) {
      this.action=this.action;
      this.files=[];
      if(file.type==="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"){
          this.files.push(file);
      }
      else{
         this.$Notice.warning({
        title: "系统提示",
        desc: "文件" + file.name + "的格式不正确，请使用下载的模板文件."
      });
      }
    
      return false;
    },
    startUpload() {
      if (this.files && this.files.length > 0) {
        this.loadingStatus = true;
        for (let i = 0; i < this.files.length; i++) {
          let item = this.files[i];
          this.$refs.upload.post(item);
        }
        this.files = [];
      
      } else {
        this.$Message.warning("当前未选择任何文件！");
      }
    },
    clearFiles() {
      this.files = [];
      this.$refs.upload.clearFiles();
    },
    onError(error, file, fileList) {
      console.log("错误");
      console.log(error);
    },
    handleFormatError(file) {
      this.$Notice.warning({
        title: "系统提示",
        desc: "文件" + file.name + "的格式不正确，请使用下载的模板文件."
      });
    },
    handleMaxSize(file) {
      this.$Notice.warning({
        title: "文件大小超出限制",
        desc: "文件  " + file.name + " 超过20m大小."
      });
    },
    handleSuccess(response, file, fileList){
       this.$Message.success(response);
        this.loadingStatus = false;
        this.$emit("save");
    }
  },
  mounted() {}
};
</script>

