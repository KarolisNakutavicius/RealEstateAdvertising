import React from 'react'

export default function Advertisement(props) {
        return (
            <div className="advertisement" style={{width: "18rem"}}>
                <img className="card-img-top" src={`data:image/png;base64,${props.ad.image}`}
                     alt="Image cannot be loaded"/>
                <div className="card-body">
                    <h5 className="card-title">{props.ad.name}</h5>

                    <label>{`${props.ad.street} ${props.ad.number}, ${props.ad.city}`}</label>
                    <label>{props.ad.size} m²</label>

                    <label className='mt-5' style={{position: "relative", right: "0px"}}><b>{props.ad.price}</b> €</label>

                    {props.isPersonal && (
                        <label className='mt-3'><em>Owner: {props.ad.ownerEmail} </em></label>
                    )}

                    <p className="card-text mt-4">{props.ad.description}</p>
                </div>
            </div>
        )
}
