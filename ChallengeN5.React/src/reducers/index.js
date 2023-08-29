import { combineReducers } from 'redux'
import permissionReducer from './permissionSlice';


const rootReducer =combineReducers({  
    permissions: permissionReducer
})

export default rootReducer;