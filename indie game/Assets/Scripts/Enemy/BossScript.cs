using UnityEngine;
using System.Collections;

public class BossScript : EnemyBase {

    int firebreathOpening = 0;
    int maul = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	

        /* bossfight script in text instead of code:
         * (teacher turns into giant dragon)
         * dragon is full of rage, slightly insane
         * You shouldn't have survived my armies... there was no way you could survive this much resistance.
         * But it looks like i'll have to end this by myself.
         * (dragon ROARS viceriously)
         * dragon breaths fire all over the place. this will not hit the player.
         * actual fight starts here
         * 
         * PHASE 1
         * dragon casts premaul spell if the player is within range for the maul spell. this will draw a sprite on the location that it will impact
         * dragon casts maul spell that hits the area that was previously highlighted.
         * 
         * dragon prepairs to breath fire. aoe indicator shows up where he is going to breath fire.
         * dragon breaths fire in a straight line in front of him. 
         * 
         * if the dragons hitpoints fall below 75%, he goes into 
         * PHASE 2:
         * dragon can still cast the premaul/maul combination
         * 
         * dragon can still breath fire. he will now hit a bigger area. this will also happen more often.
         * if the player gets behind the dragon for a bit, he will whip his tail from side to side to try and hit the player. this will both damage the player and stun him.
         * 
         * if the dragon's health gets below 25%
         * PHASE 3:
         * in this phase the player has to nuke the dragon down as fast as possible, as his attacks got a bit more extreme. the dragon really doesnt want to lose.
         * 
         * dragon can now cast groundstomp, making a wave of fire go outwards. if the player doesnt run away instantly, he will take severe damage.
         * dragon will still cast maul, but this spell now also includes a stunn.
         * dragon will still be able to cast breath fire, but will be more likely to cast the groundstomp.
         * 
         * 
         */
	}
}
