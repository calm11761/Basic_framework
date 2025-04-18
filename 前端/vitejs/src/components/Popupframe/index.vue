<template>
    <el-button plain @click="dialogTableVisible = true">
      打开表单
    </el-button>
  
    <el-button plain @click="dialogFormVisible = true">
        打开一个嵌套对话框
    </el-button>
  
    <el-dialog v-model="dialogTableVisible" title="Shipping address" width="800">
      <el-table :data="gridData">
        <el-table-column property="date" label="日期" width="150" />
        <el-table-column property="name" label="名字" width="200" />
        <el-table-column property="address" label="地址" />
      </el-table>
    </el-dialog>
  
    <el-dialog v-model="dialogFormVisible" title="Shipping address" width="500">
      <el-form :model="form">
        <el-form-item label="文本框" :label-width="formLabelWidth">
          <el-input v-model="form.name" autocomplete="off" />
        </el-form-item>
        <el-form-item label="选择框" :label-width="formLabelWidth">
          <el-select v-model="form.region" placeholder="请选择下拉框">
            <el-option label="成功" value="0" />
            <el-option label="失败" value="1" />
          </el-select>
        </el-form-item>
      </el-form>
      <template #footer>
        <div class="dialog-footer">
          <el-button @click="dialogFormVisible = false">取消</el-button>
          <el-button type="primary" @click="dialogFormVisible = false">
            确认
          </el-button>
        </div>
      </template>
    </el-dialog>
  </template>
  
  <script lang="ts" setup>
  import { reactive, ref } from 'vue'
  
  const dialogTableVisible = ref(false)
  const dialogFormVisible = ref(false)
  const formLabelWidth = '140px'
  
  const form = reactive({
    name: '',
    region: '',
    address:'',
    date1: '',
    date2: '',
    delivery: false,
    type: [],
    resource: '',
    desc: '',
  })
  
  const gridData = [
    {
     
      name: '小白',
      address: '中国',
      date: '2016-05-02',
    },
    // {
    //     date: '2016-05-02',
    //   name: '小白',
    //   address: '这是一个对话框',
    // },
    // {
    //     date: '2016-05-02',
    //   name: '小白',
    //   address: '这是一个对话框',
    // },
    // {
    //     date: '2016-05-02',
    //   name: '小白',
    //   address: '这是一个对话框',
    // },
  ]
  </script>