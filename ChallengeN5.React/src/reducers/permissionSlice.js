import { createAsyncThunk, createEntityAdapter, createSlice } from '@reduxjs/toolkit';
import axios from 'axios';

const permissionAdapter = createEntityAdapter({});

const API_BASE_URL = 'https://localhost:7079/api';

export const getAllPermissions = createAsyncThunk(
    'Permission/Get', async () => {

    const response = await axios.get(API_BASE_URL + '/Permission');
    return response.data;
    }
);

export const requestPermission = createAsyncThunk(
    'Permission/Request', async (id) => { 

      const response = await axios.get(`${API_BASE_URL}/Permission/Request/${id}`);
    return response.data;
    }
);

export const modifyPermission = createAsyncThunk(
    'Permission/Modify', async (permissionDto) => { 

    const response = await axios.put(`${API_BASE_URL}/Permission/Modify`, permissionDto);
    return response.data;
    }
);

export const { selectAll: selectPermissions, selectById: selectReportById } =
  permissionAdapter.getSelectors((state) => state.app.permissions);

  const defaultPermission = {
    id: 0,
    nombreEmpleado: '',
    apellidoEmpleado: '',
    tipoPermiso: 0,
    fechaPermiso: new Date()
  }
  
const permissionSlice = createSlice({
  name: 'app/permissions',
  initialState: {
    list: [],
    loading: false,
    selectPermission: defaultPermission
  },
  reducers: {
    resetEditPermission: (state) => {
      state.selectPermission = defaultPermission;
    },
  },
  extraReducers: {
    [getAllPermissions.fulfilled]: (state, action) => {
      const data = action.payload;
      state.list = data;
      state.loading = false;
    },
    [getAllPermissions.pending]: (state) => {
      state.loading = true;
    },
    [getAllPermissions.rejected]: (state) => {
      state.list = [];
      state.loading = false;
    },
    [requestPermission.fulfilled]: (state, action) => {
      const data = action.payload;
      state.selectPermission = data;
      state.loading = false;
    },
    [requestPermission.pending]: (state) => {
      state.selectPermission = defaultPermission;
      state.loading = true;
    },
    [requestPermission.rejected]: (state) => {
      state.selectPermission = defaultPermission;
      state.loading = false;
    },
    
  },
});

export const {
  setEditPermission,
  resetEditPermission
} = permissionSlice.actions;

export default permissionSlice.reducer;
