//Modal para administrar los grupos a los que pertenece cada accion
//Se utiliza por ejemplo en la pagina de Quote
import React, { useEffect, useState, useCallback } from "react";
import styles from "./StockGroupsAdminModal.css";
import { ModalConfirm } from "../Common/Modals/Modals";
import ListGroup from 'react-bootstrap/ListGroup';
import Form from 'react-bootstrap/Form';




const StockGroupsAdminModal = ({ show, quoteId, quoteName, onclose, fnLoadQuote }) => {
    //Todos los grupos de la DDBB
    const [groupsList, setGroupsList] = useState([]);
    //Los grupos seleccionadòs en esta accion
    const [groupsSelectedList, setGroupsSelectedList] = useState([]);

    //Obtenemos todos los grupos de la DDBB
    const LoadGroupsList = useCallback(() => {
        fetch(`api/Groups/GetList`)
            .then(response => { return response.json() })
            .then(responseJson => {
                //Guardamos la LISTA DE GRUPOS
                setGroupsList(responseJson);
            })
            .catch(error => {
                console.log(error);
            });
    }, []);


    //Obtenemos los grupos seleccionados para esta accion
    const LoadSelectedGroupsList = useCallback(() => {
        fetch(`api/Groups/GetSelectedList?q=${quoteId}`)
            .then(response => { return response.json() })
            .then(responseJson => {
                //Guardamos la LISTA DE GRUPOS
                setGroupsSelectedList(responseJson);
            })
            .catch(error => {
                console.log(error);
            });




    }, []);



    useEffect(() => {
        //Obtenemos el listado de grupos
        LoadGroupsList();
        LoadSelectedGroupsList();
    }, [LoadGroupsList, LoadSelectedGroupsList]);



    //Hacemos click en el boton de CANCELAR
    const handleNoButtonClick = () => {
        //Recargamos los grupos seleccionados de la BBDD
        LoadSelectedGroupsList();
        //Cerramos el modal
        onclose();
    }

    //Hacemos click en el boton de GUARDAR
    const handleYesButtonClick = () => {
        //Guardamos los grupos en la DDBB
        const requestOptions = {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                'quoteId': quoteId,
                'groupList': groupsSelectedList
            })
        };

        fetch('api/Groups/UpdateList', requestOptions)
            .then(response => {
                //Cerramos el modal
                onclose();
                //Recarga los datos de la accion
                fnLoadQuote();
            })
            .catch(error => {
                alert(error);
            }
            );

    }

    //Actualiza la lista groupsSelectedList segun los grupos seleccionados
    const handleCheckboxChange = (group, event) => {
        //Check o uncheck
        const isChecked = event.target.checked;

        setGroupsSelectedList((prev) => {
            if (isChecked) {
                //Seleccionado (Los agregams si es que no esta)
                if (!prev.some(g => g.id === group.id)) {
                    return [...prev, group]
                }

            } else {
                //Deseleccionado
                return prev.filter(g => g.id !== group.id);
            }
        });
    }



    const CreateGroupListItem = (group) => {

        return (
            <ListGroup.Item key={group.id}>
                <div className="">
                    <Form.Check
                        type="checkbox"
                        id={group.id}
                        label={group.description}
                        checked={groupsSelectedList.some(g => g.id === group.id)}
                        onChange={(event) => handleCheckboxChange(group, event)}
                    />
                </div>
            </ListGroup.Item>

        )
    }

    //Contenido del modal con el listado de grupos
    const CreateGroupListModalContent = () => {



        return (
            <ListGroup className="StockGroupsAdminModal">
                <ListGroup.Item className="title">Seleccione los grupos para <strong>{quoteName}</strong></ListGroup.Item>
                {
                    groupsList.map(g => (CreateGroupListItem(g)))
                }
            </ListGroup>


        )
    }




    return (
        <ModalConfirm
            show={show}
            content={CreateGroupListModalContent()}
            buttonNoTitle="Cancelar"
            onButtonNoClick={handleNoButtonClick}
            buttonYesTitle="Guardar"
            onButtonYesClick={handleYesButtonClick}
        />
    );
}


export default StockGroupsAdminModal;