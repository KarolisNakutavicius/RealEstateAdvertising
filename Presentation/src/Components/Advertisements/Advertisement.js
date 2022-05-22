import React, {useState} from 'react'
import AuthService from "../../Services/AuthService";

export default function Advertisement(props) {

    const [isSaveSuccess, setSaveSuccess] = useState(false);
    
    
    var isUserLoggedIn = AuthService.getCurrentUser() !== null;
    debugger;
    var canBeSaved = props.saveAd !== undefined && isUserLoggedIn;

    async function handleSaveAd() {
        await props.saveAd(props.ad.id);
    }

    return (
        <div className="advertisement" style={{width: "18rem"}}>
            <img className="card-img-top" src={`data:image/png;base64,${props.ad.image}`}
                 alt="Image cannot be loaded"/>
            <div className="card-body">
                <h5 className="card-title">{props.ad.name}</h5>

                <label>{`${props.ad.street} ${props.ad.number}, ${props.ad.city}`}</label>

                <label>{`${props.ad.type} for ${props.ad.isRent ? 'rent' : 'sale'}`}</label>

                <label className="mt-4"><b>Plot</b> : {props.ad.plotSize} m²</label>
                <label><b>Building</b>: {props.ad.buildingSize} m²</label>

                <label className='mt-5' style={{position: "relative", right: "0px"}}><b>{props.ad.price}</b> €</label>

                {props.isPersonal && (
                    <label className='mt-3'><em>Owner: {props.ad.ownerEmail} </em></label>
                )}

                <p className="card-text mt-4">{props.ad.description}</p>

                {canBeSaved && (
                    <button type="button" className="btn btn-primary" onClick={handleSaveAd} disabled={props.ad.isSaved}>
                        {props.ad.isSaved
                            ? (
                                <div>
                                    Saved
                                    <svg xmlns="http://www.w3.org/2000/svg" width="30" height="30" fill="currentColor"
                                         className="bi bi-check" viewBox="0 0 16 16">
                                        <path
                                            d="M10.97 4.97a.75.75 0 0 1 1.07 1.05l-3.99 4.99a.75.75 0 0 1-1.08.02L4.324 8.384a.75.75 0 1 1 1.06-1.06l2.094 2.093 3.473-4.425a.267.267 0 0 1 .02-.022z"/>
                                    </svg>
                                </div>
                            ) 
                            : ("Save")}
                    </button>
                )}
            </div>
        </div>
    )
}
