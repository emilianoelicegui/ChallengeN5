export const SET_PERMISSIONS = 'SET_PERMISSIONS';
export const ADD_PERMISSION = 'ADD_PERMISSION';
export const UPDATE_PERMISSION = 'UPDATE_PERMISSION';

export const setPermissions = (permissions) => ({
  type: SET_PERMISSIONS,
  payload: permissions,
});

export const addPermission = (permission) => ({
  type: ADD_PERMISSION,
  payload: permission,
});

export const updatePermission = (permission) => ({
  type: UPDATE_PERMISSION,
  payload: permission,
});
