/* eslint-disable no-unused-vars */
import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Button, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, Grid, Container } from '@mui/material';
import {
    getAllPermissions
  } from '../reducers/permissionSlice';
import { Link } from 'react-router-dom';
import { format } from 'date-fns';

function GetPermissionsComponent() {
  const dispatch = useDispatch();
  const permissions = useSelector(state => state.permissions);

  useEffect(() => {
    async function fetchPermissions() {
      dispatch(getAllPermissions());
    }
    
    fetchPermissions();
  }, [dispatch]);

  function showLoading (){
    return <>
      Cargando ...
    </>
  }

  return (
    <Container>
      <div>
        <Grid item xs={6}>
          <Link to="/request">
            <Button variant="contained">Nuevo Permiso</Button>
          </Link>
        </Grid>
        <Grid>
        <h2>Listado de Permisos</h2>
        </Grid>
      </div>
      {permissions.loading ? (
        showLoading()
      ) : (
                <TableContainer component={Paper}>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>ID</TableCell>
                                <TableCell>Nombre Empleado</TableCell>
                                <TableCell>Fecha Empleado</TableCell>
                                <TableCell>Fecha Permiso</TableCell>
                                <TableCell>Tipo Permiso</TableCell>
                                <TableCell></TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {permissions.list != null && permissions.list.map(permission => (
                                <tr key={permission.id}>
                                    <td>{permission.id}</td>
                                    <td>{permission.nombreEmpleado}</td>
                                    <td>{permission.apellidoEmpleado}</td>
                                    <td>{format(new Date(permission.fechaPermiso), "MM/dd/yyyy")}</td>
                                    <td>{permission.tipoPermisoNavigation.descripcion}</td>
                                    <td>
                                        <Link to={`/request/${permission.id}`}>
                                            <Button>Editar</Button>
                                        </Link>
                                    </td>
                                </tr>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            )}
    </Container>
  );
}

export default GetPermissionsComponent;