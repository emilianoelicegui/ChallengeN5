
/* eslint-disable no-case-declarations */
import initialState from './intialState';
import { SET_PERMISSIONS, ADD_PERMISSION, UPDATE_PERMISSION } from '../actions/index';

export default function permissionReducer (state = initialState.permissions, action) {
  switch (action.type) {
    case SET_PERMISSIONS:
      return {
        ...state,
        permissions: action.payload,
      };
    case ADD_PERMISSION:
      return {
        ...state,
        permissions: [...state.permissions, action.payload],
      };
    case UPDATE_PERMISSION:
      const updatedPermissions = state.permissions.map(permission =>
        permission.id === action.payload.id ? action.payload : permission
      );
      return {
        ...state,
        permissions: updatedPermissions,
      };
    default:
      return state;
  }
}