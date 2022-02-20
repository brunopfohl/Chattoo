import { Button, Card, CardActionArea, CardActions, CardContent, CardMedia, Typography } from "@mui/material";
import moment from "moment";
import { FC, useMemo } from "react";

export interface EventCardProps {
    title: string;
    description: string;
    participantsCount: number;
    maximalParticipantsCount: number;
    startsAt: Date;
    endsAt?: Date;
}

const EventCard: FC<EventCardProps> = (props) => {

    const dateText = useMemo(() => {
        let result = moment(props.startsAt).format("d. MM. YYYY - hh:mm");

        if (props.endsAt) {
            const endsAt = moment(props.endsAt).format("d. MM. YYYY - hh:mm");
            result = `Od ${result} do ${endsAt}`;
        }

        return result;
    }, [props.startsAt, props.endsAt]);

    return (
        <Card sx={{ maxWidth: 345, m: 1 }}>
            <CardActionArea>
                <CardMedia
                    component="img"
                    alt="green iguana"
                    height="140"
                    image="/event.png"
                />
                <CardContent>
                    <Typography gutterBottom variant="caption" component="div">
                        {dateText}
                    </Typography>
                    <Typography gutterBottom variant="h5" component="div">
                        {props.title}
                    </Typography>
                    <Typography gutterBottom variant="body2">
                        {props.description}
                    </Typography>
                </CardContent>
                <CardActions>
                    <Button size="small" color="primary">
                        Připojit se
                    </Button>
                </CardActions>
            </CardActionArea>
        </Card>
    );
};

EventCard.displayName = "EventsDashboardComponent";
export default EventCard;