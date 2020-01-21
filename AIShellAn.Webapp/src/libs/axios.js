import axios from 'axios'
import store from '@/store'
import { getToken } from '@/libs/util'
import iview from 'view-design'

//配置请求的header信息
axios.defaults.headers = {
  "Content-Type": "application/json;charset=UTF-8"
};

class HttpRequest {
  constructor (baseUrl = baseURL) {
    this.baseUrl = baseUrl
  }
  getInsideConfig () {
    const config = {
      baseURL: this.baseUrl,
      headers:{
        "Content-Type": "application/json;charset=UTF-8"
      }
    }
    return config
  }
 
  interceptors (instance, url) {
    // 请求拦截
    instance.interceptors.request.use(config => {
    
      let token=getToken();
       // 判断是否存在token，如果存在的话，则每个http header都加上token
      if (token) { 
        config.headers={
          "Content-Type": "application/json;charset=UTF-8",
          "Authorization":`Bearer ${token}`   
        };
      }
      return config
    }, error => {
       //可以在此处添加错误处理，记录本地日志等操作
       
      return Promise.reject(error)
    })

   
    // 响应拦截
    instance.interceptors.response.use(res => {
      const { data, status } = res;
      //Todo:判断是否无权限,或者其他错误 然后提示用户
      
      return data;
     
    }, error => {

      //可以在此处添加错误处理，记录本地日志等操作
      iview.Modal.error({
        title:"系统警告",
        content:"服务器出现异常,请联系管理员!"
      })
      return Promise.reject(error)
    });



  }
  request (options) {
    const instance = axios.create();
    options = Object.assign(this.getInsideConfig(), options);
    this.interceptors(instance, options.url)
    return instance(options)
  }
}
export default HttpRequest
