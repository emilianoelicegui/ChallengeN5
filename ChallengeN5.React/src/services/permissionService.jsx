import axios from 'axios';



const API_BASE_URL = 'https://localhost:7079/api';

export const getAllPermissions = async () => {
  const response = await axios.get(`${API_BASE_URL}/Permission/GetPermissions`);
  return response.data;
};

export const requestPermission = async (permissionDto) => {
  const response = await axios.post(`${API_BASE_URL}/Permission/RequestPermission`, permissionDto);
  return response.data;
};

export const modifyPermission = async (idPermission, permissionDto) => {
  const response = await axios.put(`${API_BASE_URL}/Permission/ModifyPermission/${idPermission}`, permissionDto);
  return response.data;
};
