import { Button } from '@mui/material';
import React from 'react';

export interface PopupProps {
    children: any;
    zIndex?: number;
    onClose: () => void;
    title: string;
}

const Popup: React.FC<PopupProps> = (props: PopupProps) => {
    const { children, onClose, title } = props;

    return (
        <>
            {/* blur */}
            <div>blur</div>
            <div>
                <div>
                    <div>
                        <div>
                            <span>{title}</span>
                        </div>
                        <div>
                            <Button onClick={onClose}>
                                Zavřít
                            </Button>
                        </div>
                    </div>
                    <div>
                        {children}
                    </div>
                </div>
            </div>
        </>
    );
}

export default Popup;