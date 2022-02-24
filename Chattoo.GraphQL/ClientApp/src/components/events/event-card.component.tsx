import { Button, Card, CardActionArea, CardActions, CardContent, CardMedia, Typography } from "@mui/material";
import { FC, useCallback, useMemo } from "react";
import moment from "moment";
import { CalendarEvent } from "graphql/graphql-types";
import { useRouter } from "next/router";

export interface EventCardProps {
    calendarEvent: CalendarEvent;
}

const EventCard: FC<EventCardProps> = (props) => {
    const router = useRouter();
    const { calendarEvent } = props;

    const dateText = useMemo(() => {
        let result = moment(calendarEvent.startsAt).format("d. MM. YYYY - hh:mm");

        if (calendarEvent.endsAt) {
            const endsAt = moment(calendarEvent.endsAt).format("d. MM. YYYY - hh:mm");
            result = `Od ${result} do ${endsAt}`;
        }

        return result;
    }, [calendarEvent]);

    const redirectToDetail = useCallback(() => {
        router.push({
            pathname: "/events/[id]",
            query: {
                id: calendarEvent.id
            }
        });
    }, [router]);

    return (
        <Card sx={{ maxWidth: 345, m: 1 }}>
            <CardActionArea onClick={redirectToDetail}>
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
                        {calendarEvent.name}
                    </Typography>
                    <Typography gutterBottom variant="body2">
                        {calendarEvent.description}
                    </Typography>
                </CardContent>
            </CardActionArea>
            <CardActions>
                <Button size="small" color="primary">
                    Připojit se
                </Button>
            </CardActions>
        </Card>
    );
};

EventCard.displayName = "EventsDashboardComponent";
export default EventCard;