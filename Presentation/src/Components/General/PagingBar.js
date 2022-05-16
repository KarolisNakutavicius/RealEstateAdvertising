import React from 'react';

function PagingBar({setPageIndex, pageIndex, totalPages}) {
    
    function handleClickPrevious() {
        let nextValue = pageIndex - 1;
        if(nextValue  > 0){
            setPageIndex(nextValue)}
    }

    function handleClickNext() {
        let nextValue = pageIndex + 1;
        if(nextValue <= totalPages){
            setPageIndex(nextValue)}
    }
    
    return (
        <nav aria-label="Page navigation example" className="pagingBar">
            <ul className="pagination">
                <li role="button" className="page-item"><a className="page-link" onClick={handleClickPrevious}>Previous</a></li>
                {Array(totalPages).fill(null).map((value, index) =>
                    <li role="button" className={pageIndex === index +1 ? "page-item active" : "page-item" }
                        key={index+1}
                        onClick={(e) => setPageIndex(index + 1)}>
                        <a className="page-link">{index + 1}</a>
                    </li>
                )
                }
                <li role="button" className="page-item" onClick={handleClickNext}><a className="page-link" >Next</a></li>
            </ul>
        </nav>
    );
}

export default PagingBar;